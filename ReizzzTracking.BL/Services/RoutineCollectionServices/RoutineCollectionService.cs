using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ReizzzTracking.BL.Errors.Auth;
using ReizzzTracking.BL.Errors.Common;
using ReizzzTracking.BL.ViewModels.Common;
using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.ResultViewModels.RoutineCollectionViewModel;
using ReizzzTracking.BL.ViewModels.RoutineCollectionViewModels;
using ReizzzTracking.DAL.Common.UnitOfWork;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.RoutineCollectionRepository;
using ReizzzTracking.DAL.Repositories.RoutineRepository;
using System.Security.Claims;

namespace ReizzzTracking.BL.Services.RoutineCollectionServices
{
    public class RoutineCollectionService : IRoutineCollectionService
    {
        private readonly IRoutineCollectionRepository _routineCollectionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRoutineRepository _routineRepository;

        public RoutineCollectionService(
            IRoutineCollectionRepository routineCollectionRepository,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            IRoutineRepository routineRepository)
        {
            _routineCollectionRepository = routineCollectionRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _routineRepository = routineRepository;
        }

        public async Task<ResultViewModel> AddRoutineCollection(RoutineCollectionAddViewModel routineCollectionVM)
        {
            var result = new ResultViewModel();
            try
            {
                var routineCollection = routineCollectionVM.ToRoutineCollection(routineCollectionVM);
                _routineCollectionRepository.Add(routineCollection);
                await _unitOfWork.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
            }
            return result;
        }

        public async Task<ResultViewModel> DeleteRoutineCollectionById(long id)
        {
            ResultViewModel result = new();
            try
            {
                RoutineCollection? routineCollectionToDelete = await _routineCollectionRepository.Find(id);
                if (routineCollectionToDelete == null)
                {
                    throw new Exception(string.Format(CommonError.NotFoundWithId, nameof(RoutineCollection), id));
                }
                _routineCollectionRepository.Remove(routineCollectionToDelete);
                await _unitOfWork.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
            }
            return result;
        }

        public async Task<RoutineCollectionGetResultViewModel> GetPaginatedRoutineCollection(GetRoutineCollectionRequestViewModel request)
        {
            //Same user see their own routine collection

            RoutineCollectionGetResultViewModel result = new RoutineCollectionGetResultViewModel();
            PaginationGetViewModel<RoutineCollectionGetViewModel> resultData = new PaginationGetViewModel<RoutineCollectionGetViewModel>
            {
                CurrentPage = request.CurrentPage,
                PageSize = request.PageSize,
                IsPaginated = request.IsPaginated,

            };
            result.PaginatedResult = resultData;
            try
            {
                var requestorIdString = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (requestorIdString == null)
                {
                    throw new Exception(AuthError.UserClaimsAccessFailed);
                }
                long requestorId = long.Parse(requestorIdString);
                (int, IEnumerable<RoutineCollection>) routineCollectionTuple = await _routineCollectionRepository.Pagination(
                                                                            request.CurrentPage,
                                                                            request.PageSize,
                                                                            expression: rc => rc.CreatedBy == requestorId,
                                                                            includeFunc: rc => rc.Include(rc => rc.CreatedByNavigation).Include(rc => rc.Routines),
                                                                            null,
                                                                            [nameof(RoutineCollection.UpdatedAt), nameof(RoutineCollection.CreatedAt)],
                                                                            [true, true]);
                resultData.TotalRecord = routineCollectionTuple.Item1;
                List<RoutineCollectionGetViewModel> listRoutineCollectionVM = new List<RoutineCollectionGetViewModel>();
                foreach (var routineCollection in routineCollectionTuple.Item2)
                {
                    RoutineCollectionGetViewModel routineCollectionVM = new RoutineCollectionGetViewModel();
                    if (routineCollectionVM.FromRoutineCollection(routineCollection) != null)
                    {
                        result.PaginatedResult.Data.Add(routineCollectionVM.FromRoutineCollection(routineCollection));
                    }
                    else
                    {
                        throw new Exception($"Routine with Id: ${routineCollection.Id} doesn't have Creator's Id");
                    }
                }
                result.PaginatedResult = resultData;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
            }
            return result;
        }

        public async Task<RoutineCollectionGetResultViewModel> GetRoutineCollectionById(long id)
        {
            RoutineCollectionGetResultViewModel result = new();
            RoutineCollectionGetViewModel resultData = new();
            try
            {
                RoutineCollection? routineCollection = await _routineCollectionRepository.FirstOrDefault(expression: rc => rc.Id == id,
                    includeFunc: rc => rc.Include(rc => rc.Routines));
                if (routineCollection == null)
                {
                    throw new Exception(string.Format(CommonError.NotFoundWithId, nameof(RoutineCollection), id));
                }
                resultData = resultData.FromRoutineCollection(routineCollection);
                result.PaginatedResult.Data.Add(resultData);
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

        public async Task<ResultViewModel> UpdateRoutineCollection(RoutineCollectionUpdateViewModel routineCollectionVM)
        {
            //TODO: maybe there's still room for improvement
            ResultViewModel result = new();
            try
            {
                RoutineCollection? routineCollectionToUpdate = await _routineCollectionRepository.Find(routineCollectionVM.Id);
                if (routineCollectionToUpdate == null)
                {
                    throw new Exception(string.Format(CommonError.NotFoundWithId, nameof(RoutineCollection), routineCollectionVM.Id));
                }
                routineCollectionToUpdate.Name = routineCollectionVM.Name;
                routineCollectionToUpdate.IsPublic = routineCollectionVM.IsPublic;
                routineCollectionToUpdate.UpdatedAt = DateTime.UtcNow;
                _routineCollectionRepository.Update(routineCollectionToUpdate);
                await _unitOfWork.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
            }
            return result;
        }
    }
}
