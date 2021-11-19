using System.IO;
using System.Threading.Tasks;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SpotMixesBlazor.Server.Settings;
using SpotMixesBlazor.Shared.ModelsData;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace SpotMixesBlazor.Server.Services
{
    public class MailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailWithVerificationCode(SendEmail sendEmail)
        {
            var filePath = Directory.GetCurrentDirectory() + "\\MailTemplates\\verify-email.html";
            var reader = new StreamReader(filePath);
            var html = await reader.ReadToEndAsync();
            reader.Close();
            html = html.Replace("{DisplayName}", sendEmail.DisplayName)
                        .Replace("{CodeVerifyEmail}", sendEmail.CodeVerifyEmail);
            
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(sendEmail.ToEmail));
            email.Subject = "Verificar correo electronico ✅";
            
            var builder = new BodyBuilder();
            builder.HtmlBody = html;
            email.Body = builder.ToMessageBody();    
            
            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;
            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}