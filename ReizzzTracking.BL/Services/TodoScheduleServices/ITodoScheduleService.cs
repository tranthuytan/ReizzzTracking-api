using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.TodoScheduleViewModels;

namespace ReizzzTracking.BL.Services.TodoScheduleServices
{
    public interface ITodoScheduleService
    {
        public Task<ResultViewModel> UserAddToDoSchedule(TodoScheduleAddViewModel todoVM);
        public Task<TodoScheduleGetResultViewModel> GetToDoScheduleById(long id);
        public Task<TodoScheduleGetResultViewModel> GetToDoSchedules(GetTodoScheduleRequestViewModel request);
        public Task<ResultViewModel> UpdateOrAddToDoSchedule(TodoScheduleUpdateViewModel[] todoVMs);
        public Task<ResultViewModel> DeleteToDoSchedules(long[] ids);
    }
}
