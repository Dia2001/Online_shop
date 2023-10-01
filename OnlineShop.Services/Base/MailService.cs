using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using OnlineShop.Core.Helpers;
using OnlineShop.Core.Models;

namespace OnlineShop.Services.Base
{
    public interface IMailService
    {
        Task SendMail(MailContentModel mailContent);
    }
    public class MailService : IMailService
    {
        private readonly MailSettings mailSettings;

        public MailService()
        {
        }

        public MailService(IOptions<MailSettings> _mailSettings)
        {
            mailSettings = _mailSettings.Value;
        }

        public async Task SendMail(MailContentModel mailContent)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Subject = mailContent.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
            }

            smtp.Disconnect(true);

        }
    }
}
