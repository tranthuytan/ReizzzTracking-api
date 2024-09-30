using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReizzzTracking.BL.Services.RoutineCollectionService;
using ReizzzTracking.BL.Services.Utils.Authentication;
using ReizzzTracking.BL.ViewModels.Common;
using ReizzzTracking.BL.ViewModels.ResultViewModels.RoutineCollectionViewModel;

namespace ReizzzTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutineCollectionController : ControllerBase
    {
        private readonly IRoutineCollectionService _routineCollectionService;

        public RoutineCollectionController(IRoutineCollectionService routineCollectionService)
        {
            _routineCollectionService = routineCollectionService;
        }
        [HttpGet("routine-collections")]
        [HasPermission(DAL.Common.Enums.Permission.AddRoutine)]
        public async Task<IActionResult> GetPaginatedRoutineCollection([FromQuery]GetRoutineCollectionRequestViewModel request)
        {
            var result = await _routineCollectionService.GetPaginatedRoutineCollection(request);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
