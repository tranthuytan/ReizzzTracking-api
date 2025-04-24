using ReizzzTracking.BL.MessageBroker.EventBus;

namespace ReizzzTracking.BL.MessageBroker.Publishers.BasePublishers
{
    public class BasePublisher
    {
        protected readonly IEventBus _eventBus;

        public BasePublisher(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
    }
}
