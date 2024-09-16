using Microsoft.AspNetCore.Identity.UI.Services;

namespace SeniorLearnDataSeed.Data.Core
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
            //throw new NotImplementedException();
        }
    }
}
