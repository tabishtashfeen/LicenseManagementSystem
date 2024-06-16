using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace LicenseManagementSystem.Helper.Functions
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string recipientName, string recipientEmail, string subject, string licenseKey, string userName)
        {
            try
            {
                string templatePath = "EmailTemplates/license-key-email.html";
                string templateContent = File.ReadAllText(templatePath);

                templateContent = templateContent.Replace("[LicenseKey]", licenseKey);
                templateContent = templateContent.Replace("[UserName]", userName);

                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(_configuration["Email:Name"], _configuration["Email:EmailAddress"]));
                emailMessage.To.Add(new MailboxAddress(recipientName, recipientEmail));
                emailMessage.Subject = subject;

                var textPart = new TextPart("plain") { Text = templateContent };
                emailMessage.Body = textPart;
                using var client = new SmtpClient();
                client.Connect(_configuration["Email:Host"], int.Parse(_configuration["Email:Port"]));
                await client.AuthenticateAsync(_configuration["Email:EmailAddress"], _configuration["Email:Password"]);
                await client.SendAsync(emailMessage);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
