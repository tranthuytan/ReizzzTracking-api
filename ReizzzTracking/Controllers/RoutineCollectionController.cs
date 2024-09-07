using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReizzzTracking.BL.Services.RoutineCollectionService;
using ReizzzTracking.BL.ViewModels.Common;

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
        public async Task<IActionResult> GetPaginatedRoutineCollection([FromQuery]GetRequestViewModel request)
        {
            var result = await _routineCollectionService.GetPaginatedRoutineCollection(request);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
