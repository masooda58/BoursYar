﻿using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Interfaces.IMessageSender;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Jwt.Identity.BoursYarServer.Areas.Account.pages
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
            [EmailAddress(ErrorMessage = "{0} وارد شده صحیح نمی باشد")]
            [Display(Name = "ایمیل")]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Account",  userEmail = Input.Email, code = code  },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Reset Password",
                    $"جهت ریست پسورد خود اینجا<a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>کلیک نمایید</a>.",true);

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}