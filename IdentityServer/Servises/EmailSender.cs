using IdentityServer.Model;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;

namespace IdentityServer.Servises
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<Smptsetting> smptsetting;

        public EmailSender(IOptions<Smptsetting> smptsetting)
        {
            this.smptsetting = smptsetting;
        }
        public async Task SendAsync(string from, string to, string subject, string body)
        {
            var message = new MailMessage(from, to, subject, body);
            using (var emailClient = new SmtpClient(smptsetting.Value.Server, smptsetting.Value.Port))
            {
                emailClient.Credentials = new NetworkCredential(
                     smptsetting.Value.Login,
                     smptsetting.Value.Password
                    );
                await emailClient.SendMailAsync(message);

            }
        }
    }
}
