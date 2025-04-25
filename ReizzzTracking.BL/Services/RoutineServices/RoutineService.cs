using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ReizzzTracking.BL.Errors.Auth;
using ReizzzTracking.BL.Errors.Common;
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

        public RoutineService(IRoutineRepository routineRepository,
            IRoutineCollectionRepository routineCollectionRepository,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork)
        {
            _routineRepository = routineRepository;
            _routineCollectionRepository = routineCollectionRepository;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> AddRoutine(RoutineAddViewModel routineVM)
        {
            var result = new ResultViewModel();
            try
            {
                string? creatorIdString = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (creatorIdString == null)
                {
                    throw new Exception(AuthError.UserClaimsAccessFailed);
                }
                Routine addRoutine = routineVM.ToRoutine(routineVM);
                addRoutine.CreatedBy = long.Parse(creatorIdString);
                if (routineVM.RoutineCollectionId != null)
                {
                    _routineRepository.Add(addRoutine);
                }
                else
                {
                    RoutineCollection addRoutineCollection = new RoutineCollection
                    {
                        Name = "New Routine Collection",
                        CreatedBy = long.Parse(creatorIdString),
                        IsPublic = false
                    };

                    addRoutine.RoutineCollectionNavigation = addRoutineCollection;
                    _routineRepository.Add(addRoutine);
                }
                await _unitOfWork.SaveChangesAsync();
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
                if (routine == null)
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
                string? creatorIdString = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (creatorIdString == null)
                {
                    throw new Exception(AuthError.UserClaimsAccessFailed);
                }


                var routines = await _routineRepository.GetAll(x => x.RoutineCollectionId == request.RoutineCollectionId, null, null, ["StartTime"], [false]);
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
                //Update existing Routine
                if (routineVM.Id != null)
                {
                    //Check if routine is existed
                    var routine = await _routineRepository.Find(routineToUpdate.Id);
                    if (routine == null)
                    {
                        throw new Exception(string.Format(CommonError.NotFoundWithId, routineToUpdate.GetType().Name, routineToUpdate.Id));
                    }
                    _routineRepository.Update(routineToUpdate, r => r.CategoryType, r => r.RoutineCollectionId);
                }
                //Create new routine if not exist
                else
                {
                    _routineRepository.Add(routineToUpdate);
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
                    if (routineToDelete == null)
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
    }
}
