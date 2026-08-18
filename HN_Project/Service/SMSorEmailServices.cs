using HN_Backend.DTOs;
using HN_Backend.Interface;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit; 
using MailKit.Net.Smtp; 

namespace HN_Backend.Service
{
    public class SMSorEmailServices: ISMSorEmailServices
    {
        private readonly EmailSettingsDto _emailSettings;
        public SMSorEmailServices(IOptions<EmailSettingsDto> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject,string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_emailSettings.SenderName,_emailSettings.SenderEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart("html")
            {
                Text = body
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer,_emailSettings.Port,SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
