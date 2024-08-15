using ReizzzTracking.BL.ViewModels.ResultViewModel;
using ReizzzTracking.BL.ViewModels.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.Services.AuthServices
{
    public interface IAuthService
    {
        public Task<ResultViewModel> Register(UserAddViewModel userAddViewModel);
    }
}
