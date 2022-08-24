using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Interfaces.IEmailSender;

namespace Jwt.Identity.BoursYarServer.Services.Emailservices
{
    public class EmailService:IEmailSender
    {
        public  Task SendEmailAsync(string toEmail,string subject, string message, bool isMessageHtml = false)
        {
            using (var client = new SmtpClient())
            {
                // قرار بدهم DataBase یا در Appsetting را بعد تصمیم می گیرم در user,Password اطلاعات
                var credentials = new NetworkCredential()
                {
                    UserName = "masoud.emulator.test", // without @gmail.com
                    Password = "Ma1234567890"
                };

                client.Credentials = credentials;
                client.Host = "smtp.gmail.com";
                client.Port =25;
                client.EnableSsl = true;

                using var emailMessage = new MailMessage()
                {
                    To = { new MailAddress(toEmail) },
                    From = new MailAddress("masoud.emulator.test @gmail.com"), // with @gmail.com
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = isMessageHtml
                };

                client.Send(emailMessage);
            }

            return Task.CompletedTask;
        }
    }
}
