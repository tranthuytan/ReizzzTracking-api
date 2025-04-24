using MassTransit;
using Microsoft.Extensions.Logging;
using ReizzzTracking.BL.MessageBroker.Publishers.RoutinePublishers;

namespace ReizzzTracking.BL.MessageBroker.Consumers.RoutineConsumers
{
    public class BackgroundRoutineCheckedEventConsumer : IConsumer<BackgroundRoutineCheckedEvent>
    {
        private readonly ILogger<BackgroundRoutineCheckedEventConsumer> _logger;

        public BackgroundRoutineCheckedEventConsumer(ILogger<BackgroundRoutineCheckedEventConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<BackgroundRoutineCheckedEvent> context)
        {
            // configue push notification
            _logger.LogInformation($"Routine id {context.Message.Id}, StartTime = {context.Message.StartTime} is checked at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
