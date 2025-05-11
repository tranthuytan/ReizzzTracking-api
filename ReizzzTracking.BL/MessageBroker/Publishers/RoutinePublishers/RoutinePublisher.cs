using ReizzzTracking.BL.MessageBroker.EventBus;
using ReizzzTracking.BL.MessageBroker.Publishers.BasePublishers;

namespace ReizzzTracking.BL.MessageBroker.Publishers.RoutinePublishers
{
    public class RoutinePublisher : BasePublisher
    {
        public RoutinePublisher(IEventBus eventBus) : base(eventBus)
        {
        }

        public async Task PublishRoutineIsEnabledCheck(BackgroundRoutineCheckedEvent backgroundRoutineCheckedEvent)
        {
            await _eventBus.PublishAsync(backgroundRoutineCheckedEvent);
        }
    }
}
