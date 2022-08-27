using Jwt.Identity.BoursYarServer.Helpers.Extensions;
using Jwt.Identity.BoursYarServer.OptionsModels;
using Jwt.Identity.Domain.Interfaces.IMessageSender;
using Jwt.Identity.Domain.Interfaces.IPhoneTotpProvider;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersianTranslation.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Jwt.Identity.BoursYarServer.Areas.Account.pages
{
    public class SendConfirmationCodeModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IPhoneTotpProvider _totp;
        private readonly ISmsSender _smsSender;
        private readonly TotpOptions _options;

        public SendConfirmationCodeModel(UserManager<ApplicationUser> userManager, ILogger<RegisterModel> logger, 
            IEmailSender emailSender, IPhoneTotpProvider totp, ISmsSender smsSender,IOptions<TotpOptions>options)
        {
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _totp = totp;
            _smsSender = smsSender;
            _options = options?.Value??new TotpOptions();
        }

        [BindProperty]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        // [EmailAddress(ErrorMessage = "{0} وارد شده صحیح نمی باشد")]
        [PhoneOrEmail]
        [Display(Name = "ایمیل یا شماره موبایل")]
        public string EmailOrPhone { get; set; }

        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (EmailOrPhone.Contains("@"))
                {
                    var userExist = await _userManager.FindByEmailAsync(EmailOrPhone);
                    if (userExist == null)
                    {
                        // ModelState.AddModelError(string.Empty, $"ایمیل {EmailOrPhone} قبلا در سایت ثبت نام نموده است");
                        return RedirectToPage("RegisterConfirmation", new { email = EmailOrPhone });
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(userExist);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Page(
                        "/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Account", userId = userExist.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(EmailOrPhone, "تاییدیه ایمیل",
                        $"جهت تایید ایمیل اینجا <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>کلیک نمایید</a>.", true);

                    return RedirectToPage("RegisterConfirmation", new { email = EmailOrPhone });

                }
                else
                {
                    //// بود 9رقم سمت راست جدا می شود Valid با توجه به
                    string normalMobileNo = "989" + EmailOrPhone.Substring(EmailOrPhone.Length - 9);
                    var userExist = await _userManager.Users
                        .AnyAsync(user => user.PhoneNumber == normalMobileNo);

                    if (!userExist)
                    {

                       // ModelState.AddModelError(string.Empty, $"شماره تلفن {normalMobileNo} قبلا در سایت ثبت نام نموده است");
                       return RedirectToPage("./Register");
                    }

                    var secretKey = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
                    var totpCode = _totp.GenerateTotp(secretKey);

                    var totpTemp = new RegisterModel.TotpTempData()
                    {
                        SecretKey = secretKey,
                        UserMobileNo = normalMobileNo,
                        ExpirationTime = DateTime.Now.AddSeconds(_options.Step)
                    };
                    TempData.Set("PTC", totpTemp);
                    var user = new ApplicationUser()
                    {
                        UserName = normalMobileNo,
                        PhoneNumber = normalMobileNo
                    };

                    await _smsSender.SendSmsAsync(normalMobileNo, totpCode, "شرکت فلان");
                    return RedirectToPage("./ConfirmMobile");

                }

            }
            ModelState.AddModelError(string.Empty,"اطلاعات وراد شده صحیح نمی باشد");
            return Page();
        }

    }
}

