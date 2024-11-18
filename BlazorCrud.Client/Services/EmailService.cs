using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Components;
using MimeKit;

namespace BlazorCrud.Client.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
            Console.WriteLine("EmailService initialized with settings: " + _emailSettings.SmtpServer);
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            Console.WriteLine($"Preparing to send email to {toEmail} with subject: {subject}");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("From", _emailSettings.FromAddress));
            message.To.Add(new MailboxAddress("To", toEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            message.Body = bodyBuilder.ToMessageBody();

            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    Console.WriteLine("Connecting to SMTP server...");
                    await smtpClient.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, true);

                    Console.WriteLine("Authenticated with SMTP server.");
                    await smtpClient.AuthenticateAsync(_emailSettings.SmtpUser, _emailSettings.SmtpPassword);
                    await smtpClient.SendAsync(message);

                    Console.WriteLine("Email sent successfully.");
                    await smtpClient.DisconnectAsync(true);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }

        }
    }

}
