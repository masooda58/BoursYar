using IdentityPersianHelper.DataAnnotations;
using Jwt.Identity.BoursYarServer.Helpers.Extensions;
using Jwt.Identity.BoursYarServer.SettingModels;
using Jwt.Identity.Domain.Interfaces.IMessageSender;
using Jwt.Identity.Domain.Interfaces.IPhoneTotpProvider;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Jwt.Identity.BoursYarServer.Resources;

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

            public string EmailOrPhone
            {
                get => _normalEmailOrPhone.ToNormalPhoneNo();
                set => _normalEmailOrPhone = value;
            }

            private string _normalEmailOrPhone;

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
                ReturnUrl = returnUrl ?? Url.Content("~/");
               
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            }

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {


            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
           

            #region Register email And mobileNo base on Visio flow

            if (ModelState.IsValid)
            {
                var userExist = await _userManager.FindByNameAsync(Input.EmailOrPhone);

                #region Check user Exist

                if (userExist != null && Input.EmailOrPhone.Contains("@"))
                {
                    ModelState.AddModelError(string.Empty,
                        $"ایمیل {Input.EmailOrPhone} قبلا در سایت ثبت نام نموده است");
                    return Page();
                }
                if (userExist != null && !Input.EmailOrPhone.Contains("@"))
                {
                    ModelState.AddModelError(string.Empty,
                        $"شماره {Input.EmailOrPhone} قبلا در سایت ثبت نام نموده است");
                    return Page();
                }
                #endregion

                #region Create user

                var user = Input.EmailOrPhone.Contains("@") ?
                    new ApplicationUser { UserName = Input.EmailOrPhone, Email = Input.EmailOrPhone } :
                    new ApplicationUser { UserName = Input.EmailOrPhone, PhoneNumber = Input.EmailOrPhone };
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, "در ساختن کاربر مشکلی رخداده است.");
                        return Page();
                    }
                    
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        TempData[TempDataDict.FromRegisterConfirmation] = Input.EmailOrPhone;
                        return RedirectToPage("/SendConfirmationCode",new {returnUrl});
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
               


                #endregion



            }

            #endregion
            // If we got this far, something failed, redisplay form
            return Page();
        }
        public async Task<JsonResult> OnPostCheckEmail()
        {

            var user = await _userManager.FindByEmailAsync(Input.EmailOrPhone);
            if (user == null)
            {
                //string normalMobileNo = "989" + Input.EmailOrPhone.Substring(Input.EmailOrPhone.Length - 9);
                user = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.PhoneNumber == Input.EmailOrPhone);
                // var validPhone = user == null;
                return new JsonResult(user == null
                    ? true
                    : $"شماره تلفن {Input.EmailOrPhone} قبلا در سایت ثبت نام نموده است");
            }
            //var validEmail = user == null;
            return new JsonResult($"ایمیل {Input.EmailOrPhone} قبلا در سایت ثبت نام نموده است");
        }
    }
}
