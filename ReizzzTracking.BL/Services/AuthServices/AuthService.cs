using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ReizzzTracking.BL.Errors.Auth;
using ReizzzTracking.BL.Services.PermissionService;
using ReizzzTracking.BL.Services.Utils.Authentication;
using ReizzzTracking.BL.Utils.PasswordHasher;
using ReizzzTracking.BL.Validators.UserValidators;
using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.ResultViewModels.AuthResultViewModel;
using ReizzzTracking.BL.ViewModels.UserViewModel;
using ReizzzTracking.DAL.Common.UnitOfWork;
using ReizzzTracking.DAL.Repositories.AuthRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserService _userService;
        public AuthService(IAuthRepository authRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IUserService userService)
        {
            _authRepository = authRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _userService = userService;
        }

        public async Task<LoginResultViewModel> Login(UserLoginViewModel userLoginViewModel)
        {
            var result = new LoginResultViewModel();
            var result1 = await _userService.GetPermissionsAsync(1);
            try
            {
                var user = await _authRepository.FirstOrDefault(u=>u.Username== userLoginViewModel.LoginUsername || u.Email==userLoginViewModel.LoginUsername);
                var isCorrectPassword = _passwordHasher.Verify(userLoginViewModel.Password, user.Password);
                //login success
                if (user != null && isCorrectPassword)
                {
                    var jwtToken = _jwtProvider.Generate(user);
                    result.jwt = jwtToken;
                    result.Success = true;
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "There's an error while login your account";
                result.Errors.Add(ex.Message);
                return result;
            }
        }

        public async Task<ResultViewModel> Register(UserRegisterViewModel userAddVM)
        {
            var result = new ResultViewModel();
            try
            {
                //checking duplicated
                if (userAddVM.Email != null)
                {
                    var isDuplicatedEmail = await _authRepository.GetUserByEmail(userAddVM.Email);
                    if (isDuplicatedEmail != null)
                    {
                        result.Message = AuthError.DuplicatedEmail;
                        return result;
                    }
                }
                var isDuplicatedUsername = await _authRepository.GetUserByUsername(userAddVM.Username);
                if (isDuplicatedUsername != null)
                {
                    result.Message = AuthError.DuplicatedUsername;
                    return result;
                }

                //hash password before add
                userAddVM.Password = _passwordHasher.Hash(userAddVM.Password);

                //save new account
                var user = userAddVM.ToUser(userAddVM);
                _authRepository.Add(user);
                await _unitOfWork.SaveChangesAsync();
                result.Success = true;
                result.Message = "Account created successful.";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "There's an error while creating your account";
                result.Errors.Add(ex.Message);
                return result;
            }
        }
    }
}
