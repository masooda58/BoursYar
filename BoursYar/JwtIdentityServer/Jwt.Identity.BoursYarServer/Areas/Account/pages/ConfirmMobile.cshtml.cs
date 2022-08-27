using Jwt.Identity.BoursYarServer.Helpers.Extensions;
using Jwt.Identity.Domain.Interfaces.IPhoneTotpProvider;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Jwt.Identity.BoursYarServer.Areas.Account.pages
{
    [AllowAnonymous]
    public class ConfirmMobileModel : PageModel
    {
        private readonly IPhoneTotpProvider _totp;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        public ConfirmMobileModel(IPhoneTotpProvider totp, UserManager<ApplicationUser> userManager, ILogger<LoginModel> logger, SignInManager<ApplicationUser> signInManager)
        {
            _totp = totp;
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }

        [BindProperty]
        [Display(Name = "کد تایید")]
        public string VerifySmsCode { get; set; }

        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (!TempData.ContainsKey("PTC"))
            {
                TempData["TotpExpire"] = "کد ارسالی منقضی شده است لطفا کد جدید دریافت کنید";
                RedirectToPage("/SendConfirmationCode");
            }
           
        }

        public async Task<ActionResult> OnPostAsync()
        {
            var ptc = TempData.Get<RegisterModel.TotpTempData>("PTC");
            if (ptc.ExpirationTime <= DateTime.Now)
            {
                TempData["TotpExpire"] ="کد ارسالی منقضی شده است لطفا کد جدید دریافت کنید";
               return RedirectToPage("./SendConfirmationCode");//send again
            }

            var mathResult = _totp.VerifyTotp(ptc.SecretKey, VerifySmsCode);

            if (mathResult.Successed)
            {
                //phone number confirmation
                var user = await _userManager.FindByNameAsync(ptc.UserMobileNo);
                if (user==null)
                {
                    TempData["TotpExpire"]="مشکلی پیش آمده مجدد درخواست کد نمایید";
                    RedirectToPage("/SendConfirmationCode");
                }
                var tokenPhone = await _userManager.GenerateChangePhoneNumberTokenAsync(user, ptc.UserMobileNo);
                var confirmMoleNumber = await _userManager.ChangePhoneNumberAsync(user, ptc.UserMobileNo, tokenPhone);
                //signin user    
                await _signInManager.SignInAsync(user, false);
                RedirectToPage(ReturnUrl);
                //SignInWithClaimsAsync(user,false,new List<Claim>()
                //{ 
                //    new Claim("MobileNo",ptc.UserMobileNo)
                //});
            }
            TempData.Keep("PTC");
            ModelState.AddModelError(string.Empty, "کد وارد شده صحیح نمی باشد");
            return Page();
        }

    }
}
