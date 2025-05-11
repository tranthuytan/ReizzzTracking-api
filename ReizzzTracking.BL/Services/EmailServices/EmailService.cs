using System.Net;
using System.Net.Mail;
using FluentEmail.Core;
using FluentEmail.Smtp;
using Google.Apis.Util;
using Microsoft.Extensions.Logging;
using ReizzzTracking.DAL.Entities;

namespace ReizzzTracking.BL.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail fluentEmail;
        private readonly ILogger<EmailService> _logger;
        public EmailService(IFluentEmail fluentEmail,
                            ILogger<EmailService> logger)
        {
            this.fluentEmail = fluentEmail;
            _logger = logger;
        }
        public async Task SendEmail(string userEmail, string subject, string body, bool isHtml)
        {
            _logger.LogInformation($"{nameof(EmailService)} sending an email");
            // Send email
            await fluentEmail
                    .To(userEmail)
                    .Subject(subject)
                    .Body(body, isHtml)
                    .SendAsync();
            _logger.LogInformation($"{nameof(EmailService)} complete sending an email");
        }
    }
}