using Microsoft.AspNetCore.Mvc;
using ReizzzTracking.DAL.Repositories.UserRepository;

namespace ReizzzTracking.Controllers
{
    [Route("api/user/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAll();
            if (users.Any())
            {
                return Ok(users);

            }
            return NotFound();
        }
    }
}