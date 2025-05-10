using MassTransit;
using Microsoft.Extensions.Logging;
using ReizzzTracking.BL.MessageBroker.Publishers.ToDoPublisher;
using ReizzzTracking.BL.Services.EmailServices;

namespace ReizzzTracking.BL.MessageBroker.Consumer.ToDoConsumers
{
    public class BackgroundToDoCheckedEventConsumer : IConsumer<BackgroundToDoCheckedEvent>
    {
        private readonly ILogger<BackgroundToDoCheckedEventConsumer> _logger;
        private readonly IEmailService _emailService;
        public BackgroundToDoCheckedEventConsumer(ILogger<BackgroundToDoCheckedEventConsumer> logger,
                                                    IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }
        public Task Consume(ConsumeContext<BackgroundToDoCheckedEvent> context)
        {
            // configue email notification
            var backgroundToDoCheckedEvent = context.Message;
            string subject = "ToDo reminder";
            string body = $"It's time to do your task in to-do list: \"{backgroundToDoCheckedEvent.Name}\" at {backgroundToDoCheckedEvent.StartAt}. This task should be done in {backgroundToDoCheckedEvent.EstimatedTime} {backgroundToDoCheckedEvent.TimeUnitString.ToLower()}s";
            _emailService.SendEmail(backgroundToDoCheckedEvent.UserEmail,
                                    subject,
                                    body,
                                    true);
            _logger.LogInformation($"Routine id {backgroundToDoCheckedEvent.Id}, StartTime = {backgroundToDoCheckedEvent.StartAtUtc.GetValueOrDefault().AddHours(7)} is checked at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}