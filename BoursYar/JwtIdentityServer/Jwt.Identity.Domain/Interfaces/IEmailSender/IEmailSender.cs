using System.Threading.Tasks;

namespace Jwt.Identity.Domain.Interfaces.IEmailSender
{
    public interface IEmailSender
    {
       
        public Task SendEmailAsync(string toEmail,string subject, string message,bool isMessageHtml=false);
    }
}
