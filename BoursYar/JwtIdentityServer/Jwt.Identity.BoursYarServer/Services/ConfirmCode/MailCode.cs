using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Interfaces.IConfirmCode;
using Jwt.Identity.Domain.Interfaces.IMessageSender;
using Jwt.Identity.Domain.Models;
using Jwt.Identity.Domain.Models.ResultModels;
using Jwt.Identity.Domain.Models.TypeCode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace Jwt.Identity.BoursYarServer.Services.ConfirmCode
{
    public class MailCode:IMailCode
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IUrlHelper _url;
        private readonly IHttpContextAccessor _httpContext;

        public MailCode(UserManager<ApplicationUser> userManager, IEmailSender emailSender, IUrlHelper url, IHttpContextAccessor context)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _url = url;
            _httpContext = context;
        }

        public async Task<ConfirmResult> SendMailCodeAsync(ApplicationUser user, MailTypeCode type)
        {

            switch (type)
            {
                case MailTypeCode.MailAccountConfirmationCode:
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

               
                    var callbackUrl = _url.Page(
                        "/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Account", email = user.Email, code = code },
                        protocol: _httpContext.HttpContext?.Request.Scheme);

                    await _emailSender.SendEmailAsync(user.Email, "تاییدیه ایمیل",
                        $"جهت تایید ایمیل اینجا <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>کلیک نمایید</a>.", true);

                    return new ConfirmResult(true, "");
                }
                case MailTypeCode.MailAccountPasswordResetCode:
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

               
                    var callbackUrl = _url.Page(
                        "/ResetPassword",
                        pageHandler: null,
                        values: new { area = "Account", userEmailOrPhone = user.Email, code },
                        protocol: _httpContext.HttpContext?.Request.Scheme);

                    await _emailSender.SendEmailAsync(
                        user.Email,
                        "Reset Password",
                        $"جهت ریست پسورد خود اینجا<a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>کلیک نمایید</a>.", true);

                    return new ConfirmResult(true, "");
                }
                default:
                    return new ConfirmResult(false, "خطای نوع ارسال");
            }
        }

     
    }
}
