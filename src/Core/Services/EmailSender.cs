using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class EmailSender : IEmailSender
    {
        // Send account's active code with gmail SMTP
        public Task SendAsync(string toEmail, string subject, string body, bool isBodyHtml = false)
        {
            using (var smtpClient = new SmtpClient())
            {
                var credentials = new NetworkCredential
                {
                    UserName = "", // My email address without @gmail.com
                    Password = "" // My email password
                };

                smtpClient.Credentials = credentials;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;

                using var mailMessage = new MailMessage()
                {
                    To = { new MailAddress(toEmail) },
                    From = new MailAddress(""), // My email address with @gmail.com
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isBodyHtml
                };

                smtpClient.Send(mailMessage);
            }

            return Task.CompletedTask;
        }
    }
}
