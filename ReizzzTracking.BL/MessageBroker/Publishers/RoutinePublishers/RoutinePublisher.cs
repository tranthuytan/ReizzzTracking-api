using ReizzzTracking.BL.MessageBroker.EventBus;
using ReizzzTracking.BL.MessageBroker.Publishers.BasePublishers;
using ReizzzTracking.DAL.Entities;

namespace ReizzzTracking.BL.MessageBroker.Publishers.RoutinePublishers
{
    public class RoutinePublisher : BasePublisher
    {
        public RoutinePublisher(IEventBus eventBus) : base(eventBus)
        {
        }

        public async Task PublishRoutineIsEnabledCheck(Routine routine)
        {
            if (routine.IsActive == true)
                await _eventBus.PublishAsync(new BackgroundRoutineCheckedEvent
                {
                    Id = routine.Id,
                    Name=routine.Name,
                    IsActive = routine.IsActive,
                    StartTime = routine.StartTime
                });
        }
    }
}
