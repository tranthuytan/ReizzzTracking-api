using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Quartz;
using ReizzzTracking.BL.BackgroundJobs.InMemoryBackgroundJobs;
using ReizzzTracking.BL.Errors.Common;
using ReizzzTracking.BL.Extensions;
using ReizzzTracking.BL.ViewModels.Common;
using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.RoutineViewModel;
using ReizzzTracking.DAL.Common.UnitOfWork;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.RoutineCollectionRepository;
using ReizzzTracking.DAL.Repositories.RoutineRepository;

namespace ReizzzTracking.BL.Services.RoutineServices
{
    public class RoutineService : IRoutineService
    {
        private readonly IRoutineRepository _routineRepository;
        private readonly IRoutineCollectionRepository _routineCollectionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISchedulerFactory _schedulerFactory;

        public RoutineService(IRoutineRepository routineRepository,
            IRoutineCollectionRepository routineCollectionRepository,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork,
            ISchedulerFactory schedulerFactory)
        {
            _routineRepository = routineRepository;
            _routineCollectionRepository = routineCollectionRepository;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _schedulerFactory = schedulerFactory;
        }

        public async Task<ResultViewModel> AddRoutine(RoutineAddViewModel routineVM)
        {
            var result = new ResultViewModel();
            try
            {
                long currentUserId = _httpContextAccessor.GetCurrentUserIdFromJwt();
                Routine addRoutine = routineVM.ToRoutine(routineVM);
                addRoutine.CreatedBy = currentUserId;
                if (routineVM.RoutineCollectionId is not null)
                {
                    _routineRepository.Add(addRoutine);
                }
                else
                {
                    RoutineCollection addRoutineCollection = new RoutineCollection
                    {
                        Name = "New Routine Collection",
                        CreatedBy = currentUserId,
                        IsPublic = false
                    };

                    addRoutine.RoutineCollectionNavigation = addRoutineCollection;
                    _routineRepository.Add(addRoutine);
                }
                await _unitOfWork.SaveChangesAsync();

                await CheckRoutineStartTimeAndSetupBackgroundJob(addRoutine);

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
                return result;
            }
            return result;
        }

        public async Task<RoutineGetResultViewModel> GetRoutineById(long id)
        {
            RoutineGetResultViewModel result = new();
            RoutineGetViewModel resultData = new();
            try
            {

                Routine? routine = await _routineRepository.Find(id);
                if (routine is null)
                    throw new Exception(string.Format(CommonError.NotFoundWithId, typeof(Routine).Name, id));
                result.PaginatedResult.Data.Add(resultData.FromRoutine(routine));
                result.PaginatedResult.IsPaginated = false;
                result.PaginatedResult.TotalRecord = 1;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
            }
            return result;
        }

        public async Task<RoutineGetResultViewModel> GetRoutines(GetRoutineRequestViewModel request)
        {
            var result = new RoutineGetResultViewModel();
            result.PaginatedResult = new PaginationGetViewModel<RoutineGetViewModel>
            {
                IsPaginated = false,
            };
            try
            {
                long currentUserId = _httpContextAccessor.GetCurrentUserIdFromJwt();

                var routines = await _routineRepository.GetAll(x => x.RoutineCollectionId == request.RoutineCollectionId
                                                                    && x.CreatedBy == currentUserId,
                                                                null,
                                                                null,
                                                                ["StartTime"],
                                                                [false]);
                result.PaginatedResult.TotalRecord = routines.Count();
                foreach (var routine in routines)
                {
                    RoutineGetViewModel routineVM = new RoutineGetViewModel();
                    result.PaginatedResult.Data.Add(routineVM.FromRoutine(routine));
                }
                result.Success = true;

            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
                return result;
            }
            return result;
        }

        public async Task<ResultViewModel> UpdateOrAddRoutine(RoutineUpdateViewModel routineVM)
        {
            ResultViewModel result = new();
            Routine routineToUpdate = routineVM.ToRoutine(routineVM);
            try
            {
                long currentUserId = _httpContextAccessor.GetCurrentUserIdFromJwt();
                routineToUpdate.CreatedBy = currentUserId;
                //Update existing Routine
                if (routineVM.Id is not null)
                {
                    //Check if routine is existed
                    var routine = await _routineRepository.Find(routineToUpdate.Id);
                    if (routine is null)
                    {
                        throw new Exception(string.Format(CommonError.NotFoundWithId, routineToUpdate.GetType().Name, routineToUpdate.Id));
                    }
                    if (routine.CreatedBy != routineToUpdate.CreatedBy)
                    {
                        throw new Exception(CommonError.NoPermissionWithThisEntity);
                    }
                    routine.StartTime = routineToUpdate.StartTime;
                    routine.Name = routineToUpdate.Name;
                    routine.IsPublic = routineToUpdate.IsPublic;
                    routine.IsActive = routineToUpdate.IsActive;
                    _routineRepository.Update(routine, r => r.CategoryType!, r => r.RoutineCollectionId!);
                    await CheckRoutineStartTimeAndSetupBackgroundJob(routine);
                }
                //Create new routine if not exist
                else
                {
                    _routineRepository.Add(routineToUpdate);
                    await CheckRoutineStartTimeAndSetupBackgroundJob(routineToUpdate);
                }
                await _unitOfWork.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
            }
            return result;
        }

        public async Task<ResultViewModel> DeleteRoutines(long[] ids)
        {
            ResultViewModel result = new();
            try
            {
                foreach (long id in ids)
                {
                    Routine? routineToDelete = await _routineRepository.Find(id);
                    if (routineToDelete is null)
                    {
                        throw new ArgumentNullException("routineToDelete", $"There's no routine with that Id = ${id}");
                    }
                    _routineRepository.Remove(routineToDelete);
                }
                await _unitOfWork.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Errors.Add(ex.Message);
            }
            return result;
        }

        private async Task CheckRoutineStartTimeAndSetupBackgroundJob(Routine routine)
        {
            string nowTimeString = DateTime.UtcNow.AddHours(7).ToString("HH:mm");
            if (string.Compare(routine.StartTime, nowTimeString) == 1)
            {
                var scheduler = await _schedulerFactory.GetScheduler();
                JobKey jobKey = JobKey.Create(nameof(JobSchedulerForNewEntity) + $"routineId-{routine.Id}", "group1");

                JobKey existingRoutineJobKey = JobKey.Create(nameof(RoutineBackgroundJobScheduler) + $"routineId-{routine.Id}", "group1");
                var jobDetail = await scheduler.GetJobDetail(existingRoutineJobKey);
                if (jobDetail is not null)
                {
                    await scheduler.DeleteJob(existingRoutineJobKey);
                }
                if (routine.IsActive == true)
                {
                    var job = JobBuilder.Create<JobSchedulerForNewEntity>()
                                                .WithIdentity(jobKey)
                                                .UsingJobData("routine", JsonConvert.SerializeObject(routine))
                                                .UsingJobData("jobType", routine.GetType().ToString())
                                                .Build();

                    var trigger = TriggerBuilder.Create()
                                                .WithIdentity(jobKey.Name, jobKey.Group)
                                                .StartNow()
                                                .Build();
                    await scheduler.ScheduleJob(job, trigger);
                }
            }
        }
    }
}
