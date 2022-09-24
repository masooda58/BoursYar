using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Models;
using Jwt.Identity.Domain.Models.ResultModels;
using Jwt.Identity.Domain.Models.TypeEnum;

namespace Jwt.Identity.Domain.Interfaces.IConfirmCode
{
    public interface IMailCode
    {
        public Task<ConfirmResult> SendMailCodeAsync(ApplicationUser user, MailTypeCode type, string callbackUri = null);

       // public Task<ConfirmResult> ConfirmMailCodeAsync(string email, string code, MailTypeCode type);
    }
}
