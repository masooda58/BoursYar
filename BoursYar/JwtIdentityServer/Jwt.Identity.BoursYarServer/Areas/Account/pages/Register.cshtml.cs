using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Jwt.Identity.BoursYarServer.Services.Emailservices;
using Jwt.Identity.Domain.Interfaces.IEmailSender;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using PersianTranslation.DataAnnotations;

namespace Jwt.Identity.BoursYarServer.Areas.Account.pages
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]

        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
           // [EmailAddress(ErrorMessage = "{0} وارد شده صحیح نمی باشد")]
           [PhoneOrEmail]
            [Display(Name = "ایمیل یا شماره موبایل")]

            [PageRemote(
                ErrorMessage = "این ایمیل قبلا استفاده شده است",
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
          
            public string Email { get; set; }

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
           // var user = new ApplicationUser { UserName ="masoud1", Email = "Masoud1@gmail.com" };
           // var result = await _userManager.CreateAsync(user, "Ma@1234567890");
           //var r=await  _userManager.Users.FirstOrDefaultAsync(u => u.EmailConfirmed == false);
           //r.EmailConfirmed = true;
           //await _userManager.UpdateAsync(r);

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
            //return RedirectToPage("Login");

            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
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

                    await _emailSender.SendEmailAsync(Input.Email, "تاییدیه ایمیل",
                        $"جهت تایید ایمیل اینجا <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>کلیک نمایید</a>.",true);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    { 
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
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

            // If we got this far, something failed, redisplay form
            return Page();
        }
        public JsonResult OnPostCheckEmail()
        {
            var user = _userManager.FindByEmailAsync(Input.Email).Result;
          
            var valid = user == null;
            return new JsonResult(valid);
        }
 
    }
}
