using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.ResultViewModels.AuthResultViewModel;
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
        public Task<ResultViewModel> Register(UserRegisterViewModel userVM);
        public Task<LoginResultViewModel> Login(UserLoginViewModel userVM);
    }
}
