using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReizzzTracking.BL.Errors.Common;
using ReizzzTracking.BL.Services.RoutineServices;
using ReizzzTracking.BL.Services.Utils.Authentication;
using ReizzzTracking.BL.Validators.RoutineValidators;
using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.RoutineViewModel;

namespace ReizzzTracking.Controllers
{
    [Route("api/routines/")]
    [ApiController]
    public class RoutineController : ControllerBase
    {
        private IRoutineService _routineService;

        public RoutineController(IRoutineService routinService)
        {
            _routineService = routinService;
        }
        [HttpPost()]
        [HasPermission(DAL.Common.Enums.Permission.AddRoutine)]
        public async Task<IActionResult> AddRoutine(RoutineAddViewModel routineVM)
        {
            ResultViewModel result = new();
            var validator = new RoutineAddValidator();
            var validation = validator.Validate(routineVM);
            if (!validation.IsValid)
            {
                result.Success = false;
                foreach (var error in validation.Errors)
                {
                    result.Errors.Add(error.ErrorMessage);
                }
                return BadRequest(result);
            }
            result = await _routineService.AddRoutine(routineVM);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("{routine_id}")]
        [HasPermission(DAL.Common.Enums.Permission.ReadRoutine)]
        public async Task<IActionResult> GetRoutineById([FromQuery] long routine_id)
        {
            var result = await _routineService.GetRoutineById(routine_id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet()]
        [HasPermission(DAL.Common.Enums.Permission.ReadRoutine)]
        public async Task<IActionResult> GetRoutines([FromQuery] GetRoutineRequestViewModel request)
        {
            var result = await _routineService.GetRoutines(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut()]
        [HasPermission(DAL.Common.Enums.Permission.UpdateRoutine)]
        public async Task<IActionResult> UpdateRoutine([FromBody] RoutineUpdateViewModel[] routinesVM)
        {
            ResultViewModel result = new();
            foreach (var routineVM in routinesVM)
            {
                var validator = new RoutineUpdateValidator();
                var validation = validator.Validate(routineVM);
                if (!validation.IsValid)
                {
                    foreach (var error in validation.Errors)
                    {
                        result.Errors.Add(error.ErrorMessage);
                    }
                    result.Success = false;
                    break;
                }
                result = await _routineService.UpdateOrAddRoutine(routineVM);
                if (result.Success == false)
                    break;
            }
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPut("{routine_id}")]
        [HasPermission(DAL.Common.Enums.Permission.UpdateRoutine)]
        public async Task<IActionResult> UpdateRoutineById([FromQuery]long routine_id, [FromBody] RoutineUpdateViewModel routineVM)
        {
            ResultViewModel result = new();
            if (routine_id != routineVM.Id)
            {
                result.Errors.Add(CommonError.IdInputMismatch);
                return BadRequest(result);
            }
            var validator = new RoutineUpdateValidator();
            var validation = validator.Validate(routineVM);
            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                {
                    result.Errors.Add(error.ErrorMessage);
                }
                result.Success = false;
            }
            result = await _routineService.UpdateOrAddRoutine(routineVM);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete()]
        [HasPermission(DAL.Common.Enums.Permission.DeleteRoutine)]
        public async Task<IActionResult> DeleteRoutines([FromQuery] long[] ids)
        {
            ResultViewModel result = new();
            result = await _routineService.DeleteRoutines(ids);
            if (result.Success)
                return Ok(result);
            return NotFound(result);
        }
        [HttpDelete("{routine_id}")]
        [HasPermission(DAL.Common.Enums.Permission.DeleteRoutine)]
        public async Task<IActionResult> DeleteRoutines(long routine_id)
        {
            ResultViewModel result = new();
            result = await _routineService.DeleteRoutines([routine_id]);
            if (result.Success)
                return Ok(result);
            return NotFound(result);
        }
    }
}
