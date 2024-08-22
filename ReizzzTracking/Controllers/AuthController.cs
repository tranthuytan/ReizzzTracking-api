using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReizzzTracking.BL.Errors.Auth;
using ReizzzTracking.BL.Services.AuthServices;
using ReizzzTracking.BL.Validators.UserValidators;
using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.ResultViewModels.AuthResultViewModel;
using ReizzzTracking.BL.ViewModels.UserViewModel;
using ReizzzTracking.DAL.Entities;

namespace ReizzzTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterViewModel userAddVM)
        {
            var result = new ResultViewModel();

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
                return BadRequest(result);
            }
            result = await _authService.Register(userAddVM);
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]UserLoginViewModel userAddVM)
        {
            var result = new LoginResultViewModel();

            result = await _authService.Login(userAddVM);
            return Ok(result);
        }
    }
}
