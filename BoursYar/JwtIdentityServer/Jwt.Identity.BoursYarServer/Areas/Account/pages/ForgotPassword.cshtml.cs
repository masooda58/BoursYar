using IdentityPersianHelper.DataAnnotations;
using Jwt.Identity.BoursYarServer.Helpers.Extensions;
using Jwt.Identity.BoursYarServer.Models.SettingModels;
using Jwt.Identity.BoursYarServer.Resources;
using Jwt.Identity.Domain.Interfaces.IMessageSender;
using Jwt.Identity.Domain.Interfaces.IPhoneTotpProvider;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Interfaces.IConfirmCode;
using Jwt.Identity.Domain.Models.TypeCode;

namespace Jwt.Identity.BoursYarServer.Areas.Account.pages
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IPhoneTotpProvider _totp;
        private readonly ISmsSender _smsSender;
        private readonly TotpSettings _options;
        private readonly ITotpCode _totpCode;
        


        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender, IPhoneTotpProvider totp, ISmsSender smsSender, IOptions<TotpSettings> options, ITotpCode totpCode)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _totp = totp;
            _smsSender = smsSender;
            _totpCode = totpCode;
            _options = options?.Value ?? new TotpSettings();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [BindProperty]
            [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
            // [EmailAddress(ErrorMessage = "{0} وارد شده صحیح نمی باشد")]
            [PhoneOrEmail]
            [Display(Name = "ایمیل یا شماره موبایل")]
            public string EmailOrPhone
            {
                get => _normalEmailOrPhone.ToNormalPhoneNo();
                set => _normalEmailOrPhone = value;
            }
            private string _normalEmailOrPhone;
        }
      

        private async Task SendEmailResetAsync(string email)
        {
            var userExist = await _userManager.FindByEmailAsync(email);

            // For more information on how to enable account confirmation and password reset please 
            // visit https://go.microsoft.com/fwlink/?LinkID=532713
            var code = await _userManager.GeneratePasswordResetTokenAsync(userExist);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/ResetPassword",
                pageHandler: null,
                values: new { area = "Account", userEmailOrPhone = Input.EmailOrPhone, code },
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(
                Input.EmailOrPhone,
                "Reset Password",
                $"جهت ریست پسورد خود اینجا<a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>کلیک نمایید</a>.", true);


            TempData[TempDataDict.ShowResetEmailMessage] = true;
        }

        public async Task OnGet()
        {


        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(Input.EmailOrPhone);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    // return RedirectToPage("./ForgotPasswordConfirmation");
                    if (Input.EmailOrPhone.Contains("@"))
                    {
                        TempData[TempDataDict.ShowResetEmailMessage] = true;
                        return Page();

                    }
                    TempData[TempDataDict.ShowTotpResetCode] = true;
                    return RedirectToPage("./ResetPassword", new { userEmailOrPhone = Input.EmailOrPhone });
                   
                }
                if (Input.EmailOrPhone.Contains("@") &&
                  !(await _userManager.IsEmailConfirmedAsync(user)))

                {
                    // Don't reveal that the user does not exist or is not confirmed
                    // return RedirectToPage("./ForgotPasswordConfirmation");

                    TempData[TempDataDict.ShowResetEmailMessage] = true;
                    return Page();

                }

                if (!Input.EmailOrPhone.Contains("@") && !(await _userManager.IsPhoneNumberConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    TempData[TempDataDict.ShowTotpResetCode] = true;
                    return RedirectToPage("./ResetPassword",new { userEmailOrPhone = Input.EmailOrPhone });
                }

                if (Input.EmailOrPhone.Contains("@"))
                {
                    await SendEmailResetAsync(Input.EmailOrPhone);
                    TempData[TempDataDict.ShowResetEmailMessage] = true;
                    return Page();
                    //return RedirectToPage("./ForgotPasswordConfirmation");
                }
                else
                {
                    // await SendPhoneResetAsync(Input.EmailOrPhone);
                    //SendResetTotpCode
                    var resultSendRestTotpCode =
                        await _totpCode.SendTotpCodeAsync(Input.EmailOrPhone,TotpTypeCode.TotpAccountPasswordResetCode);
                   
                    if (resultSendRestTotpCode.Successed)
                    {
                        TempData[TempDataDict.ShowTotpResetCode] = true;
                        TempData["phonNo"] = Input.EmailOrPhone;
                        return RedirectToPage("./ResetPassword", new { userEmailOrPhone = Input.EmailOrPhone });
                    }

                    // TempData[TempDataDict.ShowTotpResetCode]=true;
                    TempData[TempDataDict.Error_TotpCode] = resultSendRestTotpCode.ErrorMessage;
                    return Page();
                }


            }

            return Page();
        }
    }
}
