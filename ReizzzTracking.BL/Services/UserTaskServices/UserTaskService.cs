using ReizzzTracking.BL.ViewModels.ResultViewModels.UserTaskViewModel;
using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.UserTaskCollectionViewModels;
using ReizzzTracking.BL.ViewModels.UserTaskViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.Services.UserTaskServices
{
    public class UserTaskService : IUserTaskService
    {
        public Task<ResultViewModel> AddUserTask(UserTaskAddViewModel UserTaskVM);
        public Task<UserTaskGetResultViewModel> GetUserTaskById(long id);
        public Task<UserTaskGetResultViewModel> GetUserTasks(GetUserTaskRequestViewModel request);
        public Task<ResultViewModel> UpdateOrAddUserTask(UserTaskUpdateViewModel UserTaskVM);
        public Task<ResultViewModel> DeleteUserTasks(long[] ids);
    }
}
