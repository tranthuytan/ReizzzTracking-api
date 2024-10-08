using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using ReizzzTracking.BL.Services.TodoScheduleServices;
using ReizzzTracking.BL.Services.Utils.Authentication;
using ReizzzTracking.BL.Validators.TodoScheduleValidators;
using ReizzzTracking.BL.ViewModels.Common;
using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.TodoScheduleViewModels;
using System.Net;

namespace ReizzzTracking.Controllers
{
    [Route("api/todo-schedules/")]
    [ApiController]
    public class ToDoScheduleController : ControllerBase
    {
        private readonly ITodoScheduleService _todoScheduleService;
        public ToDoScheduleController(ITodoScheduleService todoScheduleService)
        {
            _todoScheduleService = todoScheduleService;
        }
        [HttpPost()]
        [HasPermission(DAL.Common.Enums.Permission.AddToDo)]
        public async Task<IActionResult> UserAddToDoSchedule(TodoScheduleAddViewModel todoVM)
        {
            TodoScheduleAddValidator validator = new();
            var validation = validator.Validate(todoVM);
            ResultViewModel result = new();
            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                {
                    result.Errors.Add(error.ErrorMessage);
                }
                return BadRequest(result);
            }
            result = await _todoScheduleService.UserAddToDoSchedule(todoVM);
            if (result.Success)
            {
                return Ok(result);
            }
            return Unauthorized(result);
        }
        [HttpGet("{todoschedule_id}")]
        [HasPermission(DAL.Common.Enums.Permission.ReadToDo)]
        public async Task<IActionResult> GetToDoById(long todoschedule_id)
        {
            var result = await _todoScheduleService.GetToDoScheduleById(todoschedule_id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [HttpGet]
        [HasPermission(DAL.Common.Enums.Permission.ReadToDo)]
        public async Task<IActionResult> GetToDos([FromQuery]GetTodoScheduleRequestViewModel request)
        {
            var result = await _todoScheduleService.GetToDoSchedules(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut]
        [HasPermission(DAL.Common.Enums.Permission.UpdateToDo)]
        public async Task<IActionResult> UpdateOrAddToDos([FromBody] TodoScheduleUpdateViewModel[] todoVMs)
        {
            var result = await _todoScheduleService.UpdateOrAddToDoSchedule(todoVMs);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete()]
        [HasPermission(DAL.Common.Enums.Permission.DeleteToDo)]
        public async Task<IActionResult> DeleteToDo([FromBody]long[] todoschedule_ids)
        {
            var result = await _todoScheduleService.DeleteToDoSchedules(todoschedule_ids);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
