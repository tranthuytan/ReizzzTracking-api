using Microsoft.EntityFrameworkCore;
using ReizzzTracking.BL.ViewModels.Common;
using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.ResultViewModels.RoutineCollectionViewModel;
using ReizzzTracking.BL.ViewModels.RoutineCollectionViewModels;
using ReizzzTracking.DAL.Common.UnitOfWork;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.RoutineCollectionRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.Services.RoutineCollectionService
{
    internal class RoutineCollectionService : IRoutineCollectionService
    {
        private readonly IRoutineCollectionRepository _routineCollectionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoutineCollectionService(IRoutineCollectionRepository routineCollectionRepository, IUnitOfWork unitOfWork)
        {
            _routineCollectionRepository = routineCollectionRepository;
            _unitOfWork = unitOfWork;
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
                throw;
            }
            return result;
        }

        public async Task<RoutineCollectionGetResultViewModel> GetPaginatedRoutineCollection(GetRequestViewModel request)
        {
            //Same user see their own routine collection
            RoutineCollectionGetResultViewModel result = new RoutineCollectionGetResultViewModel();
            PaginationGetViewModel<RoutineCollectionGetViewModel> resultData = new PaginationGetViewModel<RoutineCollectionGetViewModel>
            {
                CurrentPage = request.CurrentPage,
                PageSize = request.PageSize,
            };
            result.PaginatedResult = resultData;
            try
            {
                (int, IEnumerable<RoutineCollection>) routineCollectionTuple = await _routineCollectionRepository.Pagination(
                                                                            request.CurrentPage,
                                                                            request.PageSize,
                                                                            expression: rc => rc.CreatedBy == request.RequestedById,
                                                                            includeFunc: rc => rc.Include(rc => rc.CreatedByNavigation).Include(rc=>rc.Routines),
                                                                            null,
                                                                            [nameof(RoutineCollection.UpdatedAt),nameof(RoutineCollection.CreatedAt)],
                                                                            [true,true]);
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
                return result;
            }
            return result;
        }
    }
}
