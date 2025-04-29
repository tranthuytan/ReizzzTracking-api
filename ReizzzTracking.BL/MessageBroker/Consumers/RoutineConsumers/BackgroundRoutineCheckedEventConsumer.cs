using MassTransit;
using Microsoft.Extensions.Logging;
using ReizzzTracking.BL.MessageBroker.Publishers.RoutinePublishers;
using ReizzzTracking.BL.Services.EmailServices;

namespace ReizzzTracking.BL.MessageBroker.Consumers.RoutineConsumers
{
    public class BackgroundRoutineCheckedEventConsumer : IConsumer<BackgroundRoutineCheckedEvent>
    {
        private readonly ILogger<BackgroundRoutineCheckedEventConsumer> _logger;
        private readonly IEmailService _emailService;

        public BackgroundRoutineCheckedEventConsumer(ILogger<BackgroundRoutineCheckedEventConsumer> logger,
            IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public Task Consume(ConsumeContext<BackgroundRoutineCheckedEvent> context)
        {
            // configue push notification
            var backgroundRoutineCheckedEvent = context.Message;
            string subject = "Routine reminder";
            string body = $"It's time to do your routine: {backgroundRoutineCheckedEvent.Name} at {backgroundRoutineCheckedEvent.StartTime}";
            _emailService.SendEmail(backgroundRoutineCheckedEvent.UserEmail,
                                    subject,
                                    body,
                                    false);
            _logger.LogInformation($"Routine id {backgroundRoutineCheckedEvent.Id}, StartTime = {backgroundRoutineCheckedEvent.StartTime} is checked at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
