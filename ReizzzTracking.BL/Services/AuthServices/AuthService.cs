using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ReizzzTracking.BL.Errors.Auth;
using ReizzzTracking.BL.Validators.UserValidators;
using ReizzzTracking.BL.ViewModels.ResultViewModel;
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
        public AuthService(IAuthRepository authRepository, IUnitOfWork unitOfWork)
        {
            _authRepository = authRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultViewModel> Register(UserAddViewModel userAddVM)
        {
            var result = new ResultViewModel();
            try
            {
                var new_id = Guid.NewGuid();
                var user = userAddVM.ToUser(userAddVM);

                //validate
                var validator = new UserRegisterValidator();
                var validation = validator.Validate(userAddVM);
                if (!validation.IsValid)
                {
                    result.Success = false;
                    result.Message = AuthError.RegisterFailMessage;
                    foreach (var error in validation.Errors)
                    {
                        result.Errors.Add(error.ErrorMessage);
                    }
                    return result;
                }

                //checking duplicated
                var isDuplicatedEmail = await _authRepository.GetUserByEmail(userAddVM.Email);
                if (isDuplicatedEmail != null)
                {
                    result.Message = AuthError.DuplicatedEmail;
                    return result;
                }

                //save new account
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
