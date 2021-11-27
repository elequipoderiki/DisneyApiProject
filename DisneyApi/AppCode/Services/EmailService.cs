using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DisneyApi.AppCode.Services
{
    public interface IEMailService
    {
        Task<Response> SendMail(string targetEmail, string subject, string htmlContent = "");
    }

    public class EMailService : IEMailService
    {
        private IConfiguration _config;
        public EMailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<Response> SendMail(string targetEmail, string subject, string htmlContent)
        {
            var apiKey =  _config["SendGrid:ApiKey"];
            var client = new SendGridClient(apiKey);
            var emailSender = _config["SendGrid:EmailSender"];
            var from = new EmailAddress(emailSender);
            var subj = subject;
            var to = new EmailAddress(targetEmail);
            var plainTextContent = "Gracias por elegirnos";
            var htmlCtt = htmlContent == "" ? "<h1> Gracias por elegirnos! </h1>" : htmlContent;
            var msg = MailHelper.CreateSingleEmail(from,to,subj,plainTextContent,htmlCtt);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            return response;
        }
    }
}