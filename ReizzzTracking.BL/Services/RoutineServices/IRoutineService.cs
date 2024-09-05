using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.RoutineViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.Services.RoutineServices
{
    public interface IRoutineService
    {
        public Task<ResultViewModel> AddRoutine(RoutineAddViewModel routineVM);
    }
}
