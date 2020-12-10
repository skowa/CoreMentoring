using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Northwind.Business.Interfaces.Services;
using Northwind.Core.Options;

namespace Northwind.Business.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettingsOptions)
        {
            _smtpSettings = smtpSettingsOptions.Value;
        }

        public async Task SendAsync(string email, string subject, string htmlMessage)
        {
            var builder = new BodyBuilder { HtmlBody = htmlMessage };

            var emailMessage = new MimeMessage
            {
                Subject = subject,
                Body = builder.ToMessageBody()
            };

            emailMessage.From.Add(MailboxAddress.Parse(_smtpSettings.SenderEmail));
            emailMessage.To.Add(MailboxAddress.Parse(email));

            await SendMessageAsync(emailMessage);
        }

        private async Task SendMessageAsync(MimeMessage emailMessage)
        {
            using var smtpClient = new SmtpClient();
            if (_smtpSettings.Port.HasValue)
            {
                await smtpClient.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port.Value, useSsl: false);
            }
            else
            {
                await smtpClient.ConnectAsync(_smtpSettings.Server);
            }

            await smtpClient.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
            await smtpClient.SendAsync(emailMessage);
            await smtpClient.DisconnectAsync(true);
        }
    }
}
