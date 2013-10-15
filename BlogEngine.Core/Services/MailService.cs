using BlogEngine.Core.Infrastructure;
using System;
using System.Net;
using System.Net.Mail;

namespace BlogEngine.Core.Services
{
    public class MailService : IMailService
    {
        public bool Send(string to, string from, string subject, string body)
        {
            MailMessage message = new MailMessage();
            message.To.Add(to);
            message.Subject = subject;
            message.From = new MailAddress(from);
            message.Body = body;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com",587);
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("waslem123@gmail.com", "B4111ff$");

            smtp.Send(message);
            return true;
        }

        public void SendRegisterEmail(string to,string username, string confirmationToken)
        {
            string regBody = String.Format("Hello {0},{1}To complete the registration process click on this link http://localhost/Account/RegisterConfirmation/{2}",
                username, Environment.NewLine, confirmationToken);

            Send(to, "noreply@blogengine.com", "Complete Registration Process", regBody);
        }
    }
}
