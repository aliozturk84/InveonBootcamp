using InveonBootcamp.Business.Abstract;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using MassTransit;

namespace InveonBootcamp.Business.Concrete
{
    public class MailService(IConfiguration configuration,IPublishEndpoint publishEndpoint) : IMailService
    {
        public async Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMessageAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
            {
                mail.To.Add(to);
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new MailAddress(configuration["Mail:Username"], "Inveon Udemy", System.Text.Encoding.UTF8);

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(configuration["Mail:Username"], configuration["Mail:Password"]);
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Host = configuration["Mail:Host"];
            await smtp.SendMailAsync(mail);
        }

        public async Task SendMessageAsyncViaMassTransit(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            var emailMessage = new EmailMessage
            {
                To = tos,
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml
            };
            await publishEndpoint.Publish(emailMessage);
        }
    }
}
