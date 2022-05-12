using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace QuestionsAsked.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender()
        {
        
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string fromMail = "iridiumcodes@gmail.com";
            string fromPassword = "vwsajyadbpmjlotx";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(new MailAddress(email));
            message.Body = "<html><body>" + htmlMessage + "</html></body>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            smtpClient.Send(message);
            return Task.CompletedTask;
        }
    }
}
