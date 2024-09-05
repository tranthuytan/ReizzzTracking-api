using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReizzzTracking.BL.Services.RoutineServices;
using ReizzzTracking.BL.Services.Utils.Authentication;
using ReizzzTracking.BL.ViewModels.RoutineViewModel;

namespace ReizzzTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutineController : ControllerBase
    {
        private IRoutineService _routineService;

        public RoutineController(IRoutineService routinService)
        {
            _routineService = routinService;
        }
        [HttpPost]
        [HasPermission(DAL.Common.Enums.Permission.AddRoutine)]
        public async Task<IActionResult> AddRoutine(RoutineAddViewModel routineVM)
        {
            var result = await _routineService.AddRoutine(routineVM);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
