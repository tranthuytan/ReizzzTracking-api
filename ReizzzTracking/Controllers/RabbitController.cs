using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReizzzTracking.BL.MessageBroker.Publishers.RoutinePublishers;
using ReizzzTracking.DAL.Entities;

namespace ReizzzTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitController : ControllerBase
    {
        private readonly RoutinePublisher _routinePublisher;

        public RabbitController(RoutinePublisher routinePublisher)
        {
            _routinePublisher = routinePublisher;
        }
        // [HttpGet]
        // public async Task<IActionResult> Publish()
        // {
        //     await _routinePublisher.PublishRoutineIsEnabledCheck(new Routine
        //     {
        //         Id=1,
        //         StartTime="04:30",
        //         IsActive=true
        //     });
        //     return Ok();
        // }
    }
}
