using Jwt.Identity.BoursYarServer.Helpers.Extensions;
using Jwt.Identity.Domain.Interfaces.IMessageSender;
using Jwt.Identity.Domain.Interfaces.IPhoneTotpProvider;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PersianTranslation.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Jwt.Identity.BoursYarServer.SettingModels;
using Microsoft.Extensions.WebEncoders.Testing;

namespace Jwt.Identity.BoursYarServer.Areas.Account.pages
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IPhoneTotpProvider _totp;
        private readonly ISmsSender _smsSender;
        private readonly TotpSettings _options;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, IPhoneTotpProvider totp, ISmsSender smsSender, IOptions<TotpSettings> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _totp = totp;
            _smsSender = smsSender;
            _options = options?.Value ?? new TotpSettings();
        }

        [BindProperty]

        public InputModel Input { get; set; }
        //[TempData]
        //public TotpTempData Ptc { get; set; }

        public class TotpTempData

        {
            public byte[] SecretKey { get; set; }
            public string UserMobileNo { get; set; }
            public DateTime ExpirationTime { get; set; }
        }


        public string ReturnUrl { get; set; }
       

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
            // [EmailAddress(ErrorMessage = "{0} وارد شده صحیح نمی باشد")]
            [PhoneOrEmail]
            [Display(Name = "ایمیل یا شماره موبایل")]
            // ErrorMessage = "ایمیل یا شماره موبایل  قبلا  در سایت ثبت نام کرده است",
            [PageRemote(

                HttpMethod = "post",
                PageHandler = "CheckEmail",
                AdditionalFields = "__RequestVerificationToken"
            )]

            //[Remote(
            //    "checkemail",
            //    "WeatherForecast",
            //    ErrorMessage = "Email Address already exists",
            //    AdditionalFields = "__RequestVerificationToken",
            //    HttpMethod = "post"
            //)]

            public string EmailOrPhone { get; set; }

            [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
            [StringLength(100, ErrorMessage = "{0} حداقل {2} و حداکثر {1} باشد", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "رمز عبور")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "تکرار رمز عبور")]
            [Compare("Password", ErrorMessage = "{0} و {1} مطابقت ندارند")]
            public string ConfirmPassword { get; set; }

    
          
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            
            if (_signInManager.IsSignedIn(User))
            {
                Redirect("/");
            }
            else
            {
                returnUrl = returnUrl ?? Url.Content("~/");
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            }

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {


            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                #region register by email

                if (Input.EmailOrPhone.Contains("@"))
                {
                   
                    var userExist = await _userManager.FindByEmailAsync(Input.EmailOrPhone);
                    if (userExist != null)
                    {
                        ModelState.AddModelError(string.Empty, $"ایمیل {Input.EmailOrPhone} قبلا در سایت ثبت نام نموده است");
                        return Page();
                    }
                    var user = new ApplicationUser { UserName = Input.EmailOrPhone, Email = Input.EmailOrPhone };
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                        var callbackUrl = Url.Page(
                            "/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Account", userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.EmailOrPhone, "تاییدیه ایمیل",
                            $"جهت تایید ایمیل اینجا <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>کلیک نمایید</a>.", true);

                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = Input.EmailOrPhone });
                        }
                        else
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                #endregion register by email

                #region  register by mobile
                else
                {
                    //// بود 9رقم سمت راست جدا می شود Valid با توجه به
                    string normalMobileNo = "989" + Input.EmailOrPhone.Substring(Input.EmailOrPhone.Length - 9);
                    var userExist = await _userManager.Users
                        .AnyAsync(user => user.PhoneNumber == normalMobileNo);
                    if (userExist)
                    {
                        ModelState.AddModelError(string.Empty, $"شماره تلفن {normalMobileNo} قبلا در سایت ثبت نام نموده است");
                        return Page();
                    }

                    var secretKey = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
                    var totpCode = _totp.GenerateTotp(secretKey);
                  
                    var totpTemp = new TotpTempData()
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
                    var result = await _userManager.CreateAsync(user,Input.Password);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty,"کاربر مشکلی رخداد است");
                    }
                    await _smsSender.SendSmsAsync(normalMobileNo, totpCode, "شرکت فلان");
                    return RedirectToPage("./ConfirmMobile",new {ReturnUrl});
                    
                    //return RedirectToPage("./ConfirmMobile");
                    //  return Content(totpCode);
                }


                #endregion


            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
        public async Task<JsonResult> OnPostCheckEmail()
        {

            var user = await _userManager.FindByEmailAsync(Input.EmailOrPhone);
            if (user == null)
            {
                string normalMobileNo = "989" + Input.EmailOrPhone.Substring(Input.EmailOrPhone.Length - 9);
                user = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.PhoneNumber == normalMobileNo);
                // var validPhone = user == null;
                return new JsonResult(user == null
                    ? true
                    : $"شماره تلفن {normalMobileNo} قبلا در سایت ثبت نام نموده است");
            }
            //var validEmail = user == null;
            return new JsonResult(user == null
                ? true
                : $"ایمیل {Input.EmailOrPhone} قبلا در سایت ثبت نام نموده است");
        }






    }
}
