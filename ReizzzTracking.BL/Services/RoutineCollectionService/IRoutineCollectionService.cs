using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.RoutineCollectionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.Services.RoutineCollectionService
{
    public interface IRoutineCollectionService
    {
        public Task<ResultViewModel> AddRoutineCollection(RoutineCollectionAddViewModel routineCollectionVM);
    }
}
