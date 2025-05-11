using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.ResultViewModels.RoutineCollectionViewModel;
using ReizzzTracking.BL.ViewModels.RoutineCollectionViewModels;

namespace ReizzzTracking.BL.Services.RoutineCollectionServices
{
    public interface IRoutineCollectionService
    {
        public Task<ResultViewModel> AddRoutineCollection(RoutineCollectionAddViewModel routineCollectionVM);
        public Task<RoutineCollectionGetResultViewModel> GetPaginatedRoutineCollection(GetRoutineCollectionRequestViewModel request);
        public Task<RoutineCollectionGetResultViewModel> GetRoutineCollectionById(long id);
        public Task<ResultViewModel> UpdateRoutineCollection(RoutineCollectionUpdateViewModel routineCollectionVM);
        public Task<ResultViewModel> DeleteRoutineCollectionById(long id);
    }
}
