using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReizzzTracking.BL.Services.RoutineCollectionServices;
using ReizzzTracking.BL.Services.Utils.Authentication;
using ReizzzTracking.BL.Validators.RoutineCollectionValidators;
using ReizzzTracking.BL.ViewModels.Common;
using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.ResultViewModels.RoutineCollectionViewModel;
using ReizzzTracking.BL.ViewModels.RoutineCollectionViewModels;

namespace ReizzzTracking.Controllers
{
    [Route("api/routine-collections/")]
    [ApiController]
    public class RoutineCollectionController : ControllerBase
    {
        private readonly IRoutineCollectionService _routineCollectionService;

        public RoutineCollectionController(IRoutineCollectionService routineCollectionService)
        {
            _routineCollectionService = routineCollectionService;
        }
        [HttpGet()]
        [HasPermission(DAL.Common.Enums.Permission.AddRoutine)]
        public async Task<IActionResult> GetPaginatedRoutineCollection([FromQuery]GetRoutineCollectionRequestViewModel request)
        {
            var result = await _routineCollectionService.GetPaginatedRoutineCollection(request);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("{routinecollection_id}")]
        [HasPermission(DAL.Common.Enums.Permission.ReadRoutine)]
        public async Task<IActionResult> GetRoutineCollectionById(long routinecollection_id)
        {
            var result = await _routineCollectionService.GetRoutineCollectionById(routinecollection_id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [HttpPost]
        [HasPermission(DAL.Common.Enums.Permission.AddRoutine)]
        public async Task<IActionResult> AddRoutineCollection(RoutineCollectionAddViewModel routineCollectionVM)
        {
            RoutineCollectionAddValidator validator = new();
            ResultViewModel result = new();
            var validation = validator.Validate(routineCollectionVM);
            if (validation.IsValid)
            {
                result = await _routineCollectionService.AddRoutineCollection(routineCollectionVM);
                if (result.Success)
                {
                    return Ok(result);
                }
            }
            return BadRequest(result);
        }
        [HttpPut]
        [HasPermission(DAL.Common.Enums.Permission.UpdateRoutine)]
        public async Task<IActionResult> UpdateRoutineCollection([FromBody]RoutineCollectionUpdateViewModel routinecollectionVM)
        {
            RoutineCollectionUpdateValidator validator = new();
            ResultViewModel result = new();
            var validation = validator.Validate(routinecollectionVM);
            if (validation.IsValid)
            {
                result = await _routineCollectionService.UpdateRoutineCollection(routinecollectionVM);
                if (result.Success)
                {
                    return Ok(result);
                }
            }
            return BadRequest(result);
        }
        [HttpDelete("{routinecollection_id}")]
        [HasPermission(DAL.Common.Enums.Permission.DeleteRoutine)]
        public async Task<IActionResult> DeleteRoutineCollectionById(long routinecollection_id)
        {
            var result = await _routineCollectionService.DeleteRoutineCollectionById(routinecollection_id);

            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
