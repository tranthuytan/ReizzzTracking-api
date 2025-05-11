
using MassTransit;

namespace ReizzzTracking.BL.MessageBroker.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task PublishAsync<T>(T message) where T : class
            => _publishEndpoint.Publish(message);
    }
}
