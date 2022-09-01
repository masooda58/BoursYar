using IdentityPersianHelper.DataAnnotations;
using Jwt.Identity.BoursYarServer.Helpers.Extensions;
using Jwt.Identity.BoursYarServer.Resources;
using Jwt.Identity.BoursYarServer.SettingModels;
using Jwt.Identity.Domain.Interfaces.IMessageSender;
using Jwt.Identity.Domain.Interfaces.IPhoneTotpProvider;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Jwt.Identity.BoursYarServer.Areas.Account.pages
{
    public class SendConfirmationCodeModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IPhoneTotpProvider _totp;
        private readonly ISmsSender _smsSender;
        private readonly TotpSettings _options;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SendConfirmationCodeModel(UserManager<ApplicationUser> userManager, ILogger<RegisterModel> logger,
            IEmailSender emailSender, IPhoneTotpProvider totp, ISmsSender smsSender, IOptions<TotpSettings> options, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _totp = totp;
            _smsSender = smsSender;
            _signInManager = signInManager;
            _options = options?.Value ?? new TotpSettings();
        }

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

        [BindProperty]
        public string VerifySmsCode { get; set; }

        public string ReturnUrl { get; set; }

        public class TotpTempData

        {
            public byte[] SecretKey { get; set; }
            public string UserMobileNo { get; set; }
            public DateTime ExpirationTime { get; set; }
        }

        public async Task OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");

            if (TempData.ContainsKey(TempDataDict.FromRegisterConfirmation))
            {
                var emailOrPhone = (string)TempData[TempDataDict.FromRegisterConfirmation];
                if (emailOrPhone.Contains("@"))
                {
                    await SendEmailConfirmationAsync(emailOrPhone);
                  

                }
                else
                {

                    await SendPhoneConfirmationAsync(emailOrPhone);
                    
                }
            }
            else
            {
                if (!TempData.ContainsKey(TempDataDict.ShowEmailConfirmationMessage)
                    &&!TempData.ContainsKey(TempDataDict.ShowTotpConfirmationCode))
                {
                    TempData[TempDataDict.ShowSendCofirmationCode] = true;
                }
            }


        }

        private async Task SendPhoneConfirmationAsync(string phoneNo)
        {
            if (TempData.ContainsKey(TempDataDict.TotpConfirmationCode))
            {
                var totpConfirm = TempData.Get<TotpTempData>(TempDataDict.TotpConfirmationCode);
            }
            var secretKey = _totp.CreateSecretKey();
            var totpCode = _totp.GenerateTotp(secretKey);

            var totpTemp = new TotpTempData()
            {
                SecretKey = secretKey,
                UserMobileNo = phoneNo,
                ExpirationTime = DateTime.Now.AddSeconds(_options.Step)
            };
            TempData.Set(TempDataDict.TotpConfirmationCode, totpTemp);
            await _smsSender.SendSmsAsync(phoneNo, totpCode, "شرکت فلان");
            //just for check
            await _emailSender.SendEmailAsync(phoneNo, "sms to " + phoneNo, totpCode, false);
            TempData[TempDataDict.ShowTotpConfirmationCode]=true;
        }

        private async Task SendEmailConfirmationAsync(string email)
        {
            var userExist = await _userManager.FindByEmailAsync(email);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(userExist);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var callbackUrl = Url.Page(
                "/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Account", email = email, code = code },
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(email, "تاییدیه ایمیل",
                $"جهت تایید ایمیل اینجا <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>کلیک نمایید</a>.", true);
            TempData[TempDataDict.ShowEmailConfirmationMessage] = true;
        }
        public async Task<ActionResult> OnPostConfirmationEmailOrPhoneAsync(string returnUrl)
        {

            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                #region Send email confirmation

                if (EmailOrPhone.Contains("@"))
                {
                    var userExist = await _userManager.FindByEmailAsync(EmailOrPhone);
                    if (userExist == null)
                    {
                        TempData[TempDataDict.ShowEmailConfirmationMessage] = true;
                        ModelState.AddModelError(string.Empty, $"ایمیل {EmailOrPhone} قبلا در سایت ثبت نام نموده است");
                     //   return RedirectToPage("RegisterConfirmation", new { email = EmailOrPhone });
                     return Page();
                    }

                    await SendEmailConfirmationAsync(EmailOrPhone);
                    return Page();

                  //  return RedirectToPage("RegisterConfirmation", new { EmailOrPhone });

                }

                #endregion

                #region send sms Confirmation

                else
                {
                    if (TempData.ContainsKey(TempDataDict.TotpConfirmationCode))
                    {
                        var totpConfirm = TempData.Get<TotpTempData>(TempDataDict.TotpConfirmationCode);
                    }
                    //// بود 9رقم سمت راست جدا می شود Valid با توجه به
                    //string normalMobileNo = "989" + EmailOrPhone.Substring(EmailOrPhone.Length - 9);
                    var userExist = await _userManager.Users
                        .AnyAsync(user => user.PhoneNumber == EmailOrPhone);

                    if (!userExist)
                    {
                        TempData[TempDataDict.ShowTotpConfirmationCode] = true;
                         ModelState.AddModelError(string.Empty, $"شماره تلفن {EmailOrPhone} قبلا در سایت ثبت نام ننموده است");
                         return Page();
                    }

                    //byte[] secretKey;
                    //using (var rng =new RNGCryptoServiceProvider())
                    //{
                    //    secretKey = new byte[32];
                    //    rng.GetBytes(secretKey);
                    //}
                    //var secretKey = _totp.CreateSecretKey();
                    //var totpCode = _totp.GenerateTotp(secretKey);

                    //var totpTemp = new SendConfirmationCodeModel.TotpTempData()
                    //{
                    //    SecretKey = secretKey,
                    //    UserMobileNo = EmailOrPhone,
                    //    ExpirationTime = DateTime.Now.AddSeconds(_options.Step)
                    //};
                    //TempData.Set(TempDataDict.TotpConfirmationCode, totpTemp);
                    ////var user = new ApplicationUser()
                    ////{
                    ////    UserName = EmailOrPhone,
                    ////    PhoneNumber = EmailOrPhone
                    ////};

                    //await _smsSender.SendSmsAsync(EmailOrPhone, totpCode, "شرکت فلان");
                    // return RedirectToPage("./ConfirmMobile",new{totpCode});
                    await SendPhoneConfirmationAsync(EmailOrPhone);
                    ReturnUrl = returnUrl;
                    return Page();
                }

                #endregion


            }
            ModelState.AddModelError(nameof(VerifySmsCode), "اطلاعات وراد شده صحیح نمی باشد");
            return Page();
        }

        public async Task<ActionResult> OnPostConfirmationSmsCode(string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ReturnUrl = returnUrl;
            if (!TempData.ContainsKey(TempDataDict.TotpConfirmationCode))
            {
                TempData[TempDataDict.Error_TotpCode] = "کد ارسالی منقضی شده است لطفا کد جدید دریافت کنید";
               
                return RedirectToPage("/SendConfirmationCode",new{returnUrl});
              
            }
            var ptc = TempData.Get<SendConfirmationCodeModel.TotpTempData>(TempDataDict.TotpConfirmationCode);
            if (ptc.ExpirationTime <= DateTime.Now)
            {
                TempData[TempDataDict.Error_TotpCode] ="کد ارسالی منقضی شده است لطفا کد جدید دریافت کنید";
               // TempData.Remove(TempDataDict.TotpConfirmationCode);
                return RedirectToPage("./SendConfirmationCode",new{returnUrl});//send again
            }

            var mathResult = _totp.VerifyTotp(ptc.SecretKey, VerifySmsCode);

            if (mathResult.Successed)
            {
                //phone number confirmation
                var user = await _userManager.FindByNameAsync(ptc.UserMobileNo);
                if (user==null)
                {
                    TempData["TotpExpire"]="مشکلی پیش آمده مجدد درخواست کد نمایید";
                    return RedirectToPage("/SendConfirmationCode",new{returnUrl});
                }
                var tokenPhone = await _userManager.GenerateChangePhoneNumberTokenAsync(user, ptc.UserMobileNo);
                var confirmMoleNumber = await _userManager.ChangePhoneNumberAsync(user, ptc.UserMobileNo, tokenPhone);
                //signin user    
                await _signInManager.SignInAsync(user, false);
                return LocalRedirect(ReturnUrl);
                //SignInWithClaimsAsync(user,false,new List<Claim>()
                //{ 
                //    new Claim("MobileNo",ptc.UserMobileNo)
                //});
               
            }

           
            TempData.Keep(TempDataDict.TotpConfirmationCode);
            TempData[TempDataDict.ShowTotpConfirmationCode] = true;
            TempData[TempDataDict.Error_TotpCode] = "کد وارد شده صحیح نمی باشد";
           
            
            return RedirectToPage("./SendConfirmationCode",new{returnUrl});
        }
    }
}

