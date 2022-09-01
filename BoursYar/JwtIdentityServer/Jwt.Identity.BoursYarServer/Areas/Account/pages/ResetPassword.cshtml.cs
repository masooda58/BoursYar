using Jwt.Identity.BoursYarServer.Helpers.Extensions;
using Jwt.Identity.BoursYarServer.Resources;
using Jwt.Identity.Domain.Interfaces.IPhoneTotpProvider;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Identity.BoursYarServer.Areas.Account.pages
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPhoneTotpProvider _totp;


        public ResetPasswordModel(UserManager<ApplicationUser> userManager, IPhoneTotpProvider totp)
        {
            _userManager = userManager;
            _totp = totp;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public class TotpTempData

        {
            public byte[] SecretKey { get; set; }
            public string UserMobileNo { get; set; }
            public DateTime ExpirationTime { get; set; }
        }
        public class InputModel
        {

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
            [Required]
            [Display(Name = "کد تایید")]
            public string Code { get; set; }
        }

        public IActionResult OnGet(string code = null, string userEmailOrPhone = null)
        {
            // 
            if (!TempData.ContainsKey(TempDataDict.ShowTotpResetCode))
            {
                if (code == null || userEmailOrPhone == null)
                {
                    return BadRequest("کد تایید ایمیل اشتباه است");
                }
                else
                {
                    Input = new InputModel
                    {
                        EmailOrPhone = userEmailOrPhone,
                        Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                    };
                    return Page();
                }
            }

            if (userEmailOrPhone == null)
            {
                return BadRequest("کد تایید ایمیل اشتباه است");
            }

            //اگر پارمتر ورودی وجود داشته باشد و کاربر آن را دستی نزده باشد
            Input = new InputModel
            {
                EmailOrPhone = userEmailOrPhone,
                Code = ""
            };

            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            #region reset password by Email
            var user = await _userManager.FindByNameAsync(Input.EmailOrPhone);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./Login");
            }

            if (Input.EmailOrPhone.Contains("@"))
            {
                var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);

                if (result.Succeeded)
                {
                    return RedirectToPage("./Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return Page();
            }


            #endregion


            #region reset password by Totp
            var resultConfirmRestTotpCode = await ConfirmResetTotpCod();
            if (resultConfirmRestTotpCode.Successed)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, code, Input.Password);
                if (result.Succeeded)
                {
                    return RedirectToPage("./Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return Page();

            }

            if (TempData.ContainsKey(TempDataDict.Error_TotpCode))
            {
                return RedirectToPage("./ForgotPassword");
            }

            ModelState.AddModelError(String.Empty, resultConfirmRestTotpCode.ErrorMessage);
            return Page();


            #endregion


        }

        private async Task<PhoneTotpResult> ConfirmResetTotpCod()
        {
            // اگر سیستم کدی را ارسال نکرده باشد
            if (!TempData.ContainsKey(TempDataDict.TotpResetCode))
            {
                TempData[TempDataDict.Error_TotpCode] = "کد ارسالی منقضی شده است لطفا کد جدید دریافت کنید";
                return new PhoneTotpResult(false, "کد ارسالی منقضی شده است لطفا کد جدید دریافت کنید");

            }
            // اگر کئ ارسالی منقضی شده باشد
            var resetCodeTemp = TempData.Get<TotpTempData>(TempDataDict.TotpResetCode);
            if (resetCodeTemp.ExpirationTime <= DateTime.Now)
            {
                TempData[TempDataDict.Error_TotpCode] = "کد ارسالی منقضی شده است لطفا کد جدید دریافت کنید";
                return new PhoneTotpResult(false, "کد ارسالی منقضی شده است لطفا کد جدید دریافت کنید");

            }


            var mathResult = _totp.VerifyTotp(resetCodeTemp.SecretKey, Input.Code);
            //اگر کد درست باشد
            if (mathResult.Successed)
            {
                //phone number confirmation
                var user = await _userManager.FindByNameAsync(resetCodeTemp.UserMobileNo);
                if (user == null)
                {
                    TempData[TempDataDict.Error_TotpCode] = "مشکلی پیش آمده مجدد درخواست کد نمایید";
                    return new PhoneTotpResult(false, "مشکلی پیش آمده مجدد درخواست کد نمایید");

                }

                return new PhoneTotpResult(true, "");

            }

            // اگر کد غلط باشد
            TempData.Keep(TempDataDict.TotpResetCode);
            TempData[TempDataDict.ShowTotpResetCode] = true;
            //TempData[TempDataDict.Error_TotpCode] = "کد وارد شده صحیح نمی باشد";


            return new PhoneTotpResult(false, "کد وارد شده صحیح نمی باشد");
        }

    }
}
