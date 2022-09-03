using Jwt.Identity.Domain.Interfaces.IMessageSender;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IdentityPersianHelper.DataAnnotations;
using Jwt.Identity.BoursYarServer.Helpers.Extensions;

namespace Jwt.Identity.BoursYarServer.Areas.Account.pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IEmailSender _emailSender;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

  

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
            // [EmailAddress(ErrorMessage = "{0} وارد شده صحیح نمی باشد")]
            [PhoneOrEmail]
            [Display(Name = "ایمیل یا شماره موبایل")]
            // ErrorMessage = "ایمیل یا شماره موبایل  قبلا  در سایت ثبت نام کرده است",
            public string EmailOrPhone
            {
                get=>_normalEmailOrPhone.ToNormalPhoneNo();
                set=>_normalEmailOrPhone=value;
            }

            private string _normalEmailOrPhone;
          
            [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
            [DataType(DataType.Password)]
            [Display(Name = "رمز عبور")]
            public string Password { get; set; }

            [Display(Name = "مرا بخاطر بسپار")]
            public bool RememberMe { get; set; }
        }

        public async Task<ActionResult> OnGetAsync(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
            {
             return   RedirectToPage("/");
            }
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
               
                var user = await _userManager.FindByNameAsync(Input.EmailOrPhone);
                if (user==null)
                {
                    ModelState.AddModelError(string.Empty, " نام کاربری یا رمز عبور اشتباه است. ");
                    return Page();
                }

                var result = await _signInManager.PasswordSignInAsync(Input.EmailOrPhone, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                //if (result.RequiresTwoFactor)
                //{
                //    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                //}
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    if (user.PhoneNumberConfirmed || user.EmailConfirmed)
                    {
                        ModelState.AddModelError(string.Empty, " نام کاربری یا رمز عبور اشتباه است. ");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "اکانت کاربر تایید نشده است لطفا نسبت به تایید اکانت اقدام فرمایید");
                    }


                    return Page();
                }




            }

            // If we got this far, something failed, redisplay form
            return Page();
        }




    }
}
