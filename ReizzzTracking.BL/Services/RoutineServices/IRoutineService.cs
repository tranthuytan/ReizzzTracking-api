using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.RoutineViewModel;

namespace ReizzzTracking.BL.Services.RoutineServices
{
    public interface IRoutineService
    {
        public Task<ResultViewModel> AddRoutine(RoutineAddViewModel routineVM);
        public Task<RoutineGetResultViewModel> GetRoutineById(long id);
        public Task<RoutineGetResultViewModel> GetRoutines(GetRoutineRequestViewModel request);
        public Task<ResultViewModel> UpdateOrAddRoutine(RoutineUpdateViewModel routineVM);
        public Task<ResultViewModel> DeleteRoutines(long[] ids);
    }
}
