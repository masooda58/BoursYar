using Jwt.Identity.Api.Server.Resources;
using Jwt.Identity.Domain.Interfaces.IConfirmCode;
using Jwt.Identity.Domain.Interfaces.IMessageSender;
using Jwt.Identity.Domain.Models;
using Jwt.Identity.Domain.Models.ResultModels;
using Jwt.Identity.Domain.Models.TypeEnum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Jwt.Identity.Api.Server.Services.ConfirmCode
{
    public class MailCode : IMailCode
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        private readonly IHttpContextAccessor _httpContext;

        public MailCode(UserManager<ApplicationUser> userManager, IEmailSender emailSender, IHttpContextAccessor context)
        {
            _userManager = userManager;
            _emailSender = emailSender;

            _httpContext = context;
        }

        public async Task<ConfirmResult> SendMailCodeAsync(ApplicationUser user, MailTypeCode type, string callbackUri = null)
        {
            if (callbackUri == null)
            {
                return new ConfirmResult(false, MessageRes.CallBackUrlNotValid);
            }
            switch (type)
            {
                case MailTypeCode.MailAccountConfirmationCode:
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                  

                        var callbackUrl = callbackUri + $"?email={user.Email}&code={code}";




                        await _emailSender.SendEmailAsync(user.Email, "تاییدیه ایمیل",
                            $"جهت تایید ایمیل اینجا <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>کلیک نمایید</a>.", true);

                        return new ConfirmResult(true, MessageRes.EmailSent);
                    }
                case MailTypeCode.MailAccountPasswordResetCode:
                    {
                        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));


                        var callbackUrl = callbackUri + $"?email={user.Email}&code={code}";


                        await _emailSender.SendEmailAsync(
                            user.Email,
                            "Reset Password",
                            $"جهت ریست پسورد خود اینجا<a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>کلیک نمایید</a>.", true);

                        return new ConfirmResult(true, MessageRes.EmailSent);
                    }
                default:
                    return new ConfirmResult(false, MessageRes.UnkonwnError);
            }
        }


    }
}
