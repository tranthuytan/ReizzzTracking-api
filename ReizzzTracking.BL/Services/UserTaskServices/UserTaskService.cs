using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.UserTaskViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.Services.UserTaskServices
{
    public class UserTaskService : IUserTaskService
    {
        public Task<ResultViewModel> AddUserTask(UserTaskAddViewModel userTaskVM)
        {
            throw new NotImplementedException();
        }

        public Task<ResultViewModel> DeleteUserTasks(long[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<UserTaskGetResultViewModel> GetUserTaskById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<UserTaskGetResultViewModel> GetUserTasks(GetUserTaskRequestViewModel request)
        {
            throw new NotImplementedException();
        }

        public Task<ResultViewModel> UpdateOrAddUserTask(UserTaskUpdateViewModel userTaskVM)
        {
            throw new NotImplementedException();
        }
    }
}
