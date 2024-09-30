

using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.UserTaskViewModels;

namespace ReizzzTracking.BL.Services.UserTaskServices
{
    public interface IUserTaskService
    {
        public Task<ResultViewModel> AddUserTask(UserTaskAddViewModel UserTaskVM);
        public Task<UserTaskGetResultViewModel> GetUserTaskById(long id);
        public Task<UserTaskGetResultViewModel> GetUserTasks(GetUserTaskRequestViewModel request);
        public Task<ResultViewModel> UpdateOrAddUserTask(UserTaskUpdateViewModel UserTaskVM);
        public Task<ResultViewModel> DeleteUserTasks(long[] ids);
    }
}
