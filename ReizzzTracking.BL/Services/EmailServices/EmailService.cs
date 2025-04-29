using System.Net;
using System.Net.Mail;
using FluentEmail.Core;
using FluentEmail.Smtp;
using Google.Apis.Util;
using ReizzzTracking.DAL.Entities;

namespace ReizzzTracking.BL.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail fluentEmail;
        public EmailService(IFluentEmail fluentEmail)
        {
            this.fluentEmail = fluentEmail;
        }
        public async Task SendEmail(string userEmail, string subject, string body, bool isHtml)
        {
            // Send email
            await fluentEmail
                    .To(userEmail)
                    .Subject(subject)
                    .Body(body, isHtml)
                    .SendAsync();

        }
    }
}