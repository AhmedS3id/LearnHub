using Microsoft.AspNetCore.Identity.UI.Services;

namespace LearnHub_Api.Services
{
    public class EmailServices : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }
    }
}
