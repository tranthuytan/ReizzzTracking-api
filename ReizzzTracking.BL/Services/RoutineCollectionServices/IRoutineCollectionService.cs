using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.ResultViewModels.RoutineCollectionViewModel;
using ReizzzTracking.BL.ViewModels.RoutineCollectionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
