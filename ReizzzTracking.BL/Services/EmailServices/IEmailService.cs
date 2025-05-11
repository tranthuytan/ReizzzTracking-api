namespace ReizzzTracking.BL.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendEmail(string userEmail, string subject, string body, bool isHtml);
    }
}