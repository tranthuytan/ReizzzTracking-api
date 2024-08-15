using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReizzzTracking.BL.Services.AuthServices;
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
        [HttpPost]
        public async Task<IActionResult> Register(UserAddViewModel userAddVM)
        {
            var result = await _authService.Register(userAddVM);
            return Ok(result);
        }
    }
}
