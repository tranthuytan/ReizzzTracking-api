using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Quartz;
using ReizzzTracking.BL.BackgroundJobs.InMemoryBackgroundJobs;
using ReizzzTracking.BL.Errors.Common;
using ReizzzTracking.BL.Extensions;
using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.TodoScheduleViewModels;
using ReizzzTracking.BL.ViewModels.ToDoScheduleViewModelsToDoSchedule;
using ReizzzTracking.DAL.Common.UnitOfWork;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.TodoScheduleRepository;

namespace ReizzzTracking.BL.Services.TodoScheduleServices
{
    public class TodoScheduleService : ITodoScheduleService
    {
        private readonly ITodoScheduleRepository _todoScheduleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISchedulerFactory _schedulerFactory;
        public TodoScheduleService(ITodoScheduleRepository todoScheduleRepository,
                                    IUnitOfWork unitOfWork,
                                    IHttpContextAccessor httpContextAccessor,
                                    ISchedulerFactory schedulerFactory)
        {
            _todoScheduleRepository = todoScheduleRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _schedulerFactory = schedulerFactory;
        }
        public async Task<ResultViewModel> UserAddToDoSchedule(TodoScheduleAddViewModel todoVM)
        {
            ResultViewModel result = new();
            try
            {
                long userId = _httpContextAccessor.GetCurrentUserIdFromJwt();


                TodoSchedule toDo = todoVM.ToToDoSchedule(todoVM);
                toDo.AppliedFor = userId;
                toDo.CategoryType = CategoryType.ToDo.Id;
                toDo.IsDone = false;

                _todoScheduleRepository.Add(toDo);
                await _unitOfWork.SaveChangesAsync();
                await CheckToDoStartTimeAndSetupBackgroundJob(toDo);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
            }
            return result;
        }

        public async Task<ResultViewModel> DeleteToDoSchedules(long[] ids)
        {
            ResultViewModel result = new();
            long currentUserId = _httpContextAccessor.GetCurrentUserIdFromJwt();
            try
            {
                foreach (var id in ids)
                {
                    TodoSchedule? toDoScheduleForDelete = await _todoScheduleRepository.Find(id);
                    if (toDoScheduleForDelete is null)
                    {
                        throw new Exception(string.Format(CommonError.NotFoundWithId, nameof(TodoSchedule), id));
                    }
                    if (toDoScheduleForDelete.AppliedFor != currentUserId)
                    {
                        throw new Exception(CommonError.NoPermissionWithThisEntity);
                    }
                    _todoScheduleRepository.Remove(toDoScheduleForDelete);
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

        public async Task<TodoScheduleGetResultViewModel> GetToDoScheduleById(long id)
        {
            TodoScheduleGetResultViewModel result = new TodoScheduleGetResultViewModel();
            try
            {
                TodoSchedule? todo = await _todoScheduleRepository.Find(id);
                if (todo is null)
                {
                    throw new Exception(string.Format(CommonError.NotFoundWithId, nameof(TodoSchedule), id));
                }
                TodoScheduleGetViewModel todoScheduleGetViewModel = new TodoScheduleGetViewModel();
                todoScheduleGetViewModel = todoScheduleGetViewModel.FromToDoSchedule(todo);
                result.PaginatedResult.Data.Add(todoScheduleGetViewModel);
                result.PaginatedResult.IsPaginated = false;
                result.PaginatedResult.TotalRecord = 1;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Errors.Add(ex.Message);
            }
            return result;
        }

        public async Task<TodoScheduleGetResultViewModel> GetToDoSchedules(GetTodoScheduleRequestViewModel request)
        {
            TodoScheduleGetResultViewModel result = new TodoScheduleGetResultViewModel();
            result.PaginatedResult.IsPaginated = request.IsPaginated;
            result.PaginatedResult.CurrentPage = request.CurrentPage;
            result.PaginatedResult.PageSize = request.PageSize;

            long currentUserId = _httpContextAccessor.GetCurrentUserIdFromJwt();
            try
            {
                var todoSchedules = await _todoScheduleRepository.Pagination(request.CurrentPage,
                                                                                request.PageSize,
                                                                                td => td.AppliedFor == currentUserId,
                                                                                null,
                                                                                null,
                                                                                [nameof(TodoSchedule.StartAtUtc)],
                                                                                [false]);
                result.PaginatedResult.TotalRecord = todoSchedules.Item1;
                foreach (var todoSchedule in todoSchedules.Item2)
                {
                    TodoScheduleGetViewModel todoVM = new();
                    result.PaginatedResult.Data.Add(todoVM.FromToDoSchedule(todoSchedule));
                }
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Errors.Add(ex.Message);
            }
            return result;
        }

        public async Task<ResultViewModel> UpdateToDoSchedule(TodoScheduleUpdateViewModel[] toDoVMs)
        {
            ResultViewModel result = new();

            var currentUserId = _httpContextAccessor.GetCurrentUserIdFromJwt();

            foreach (var toDoVM in toDoVMs)
            {
                // Check if Todo is existed
                try
                {
                    TodoSchedule? toDo = await _todoScheduleRepository.Find(toDoVM.Id);
                    if (toDo is null)
                    {
                        throw new Exception(CommonError.NotFoundWithId);
                    }
                    if (toDo.AppliedFor != currentUserId)
                    {
                        throw new Exception(CommonError.NoPermissionWithThisEntity);
                    }

                    //Complete Todo
                    if (toDo.IsDone == false && toDoVM.IsDone == true)
                    {
                        toDo.EndAtUtc = DateTime.UtcNow;
                        toDo.ActualTime = TimeDifferenceBetweenStartAtAndEndAtByTimeUnit((long)toDoVM.TimeUnitId!, (DateTime)toDo.StartAtUtc!, (DateTime)toDo.EndAtUtc!);
                    }

                    //Update Todo
                    toDo.Name = toDoVM.Name;
                    toDo.StartAtUtc = toDoVM.StartAt.GetValueOrDefault().AddHours(-7);
                    toDo.IsDone = toDoVM.IsDone;
                    toDo.EstimatedTime = toDoVM.EstimatedTime;
                    toDo.TimeUnitId = toDoVM.TimeUnitId;
                    _todoScheduleRepository.Update(toDo, td => td.CategoryType!);

                    await _unitOfWork.SaveChangesAsync();

                    // add background job to notify when the todo should be started
                    await CheckToDoStartTimeAndSetupBackgroundJob(toDo);
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Errors.Add(ex.Message);
                }
            }
            return result;
        }
        public decimal TimeDifferenceBetweenStartAtAndEndAtByTimeUnit(long timeUnitId, DateTime startAt, DateTime endAt)
        {
            TimeSpan timeDifference = endAt - startAt;
            decimal result = 0;
            if (timeUnitId == TimeUnit.Second.Id)
            {
                result = (decimal)timeDifference.TotalSeconds;
            }
            if (timeUnitId == TimeUnit.Minute.Id)
            {
                result = (decimal)timeDifference.TotalMinutes;
            }
            if (timeUnitId == TimeUnit.Hour.Id)
            {
                result = (decimal)timeDifference.TotalHours;
            }
            return result;
        }

        private async Task CheckToDoStartTimeAndSetupBackgroundJob(TodoSchedule toDo)
        {
            if (DateTime.Compare(toDo.StartAtUtc, DateTime.UtcNow) == 1)
            {
                var scheduler = await _schedulerFactory.GetScheduler();
                JobKey jobKey = JobKey.Create(nameof(JobSchedulerForNewEntity) + $"toDoId-{toDo.Id}", "group1");

                await RemoveBackgroundJobWhenToDoIsDone(toDo);
                if (toDo.IsDone == false)
                {
                    var job = JobBuilder.Create<JobSchedulerForNewEntity>()
                                                .WithIdentity(jobKey)
                                                .UsingJobData("toDo", JsonConvert.SerializeObject(toDo))
                                                .UsingJobData("jobType", toDo.GetType().ToString())
                                                .Build();

                    var trigger = TriggerBuilder.Create()
                                                .WithIdentity(jobKey.Name, jobKey.Group)
                                                .StartNow()
                                                .Build();
                    await scheduler.ScheduleJob(job, trigger);
                }
            }
        }
        private async Task RemoveBackgroundJobWhenToDoIsDone(TodoSchedule toDo)
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            JobKey existingToDoJobKey = JobKey.Create(nameof(TodoScheduleBackgroundJob) + $"toDoId-{toDo.Id}", "group1");
            var jobDetail = await scheduler.GetJobDetail(existingToDoJobKey);
            if (jobDetail is not null)
            {
                await scheduler.DeleteJob(existingToDoJobKey);
            }
        }
    }
}
