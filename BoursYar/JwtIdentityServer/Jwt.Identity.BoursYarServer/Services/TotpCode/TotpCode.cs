using System;
using System.Threading.Tasks;
using Jwt.Identity.BoursYarServer.Models.SettingModels;
using Jwt.Identity.BoursYarServer.Resources;
using Jwt.Identity.Domain.Interfaces.IMessageSender;
using Jwt.Identity.Domain.Interfaces.IPhoneTotpProvider;
using Jwt.Identity.Domain.Interfaces.ISendPhoneCode;
using Jwt.Identity.Domain.Models;
using Jwt.Identity.Domain.Models.ResultModels;
using Jwt.Identity.Domain.Models.TransferData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Jwt.Identity.BoursYarServer.Services.TotpCode
{
    public class TotpCode : ITotpCode
    {
        #region Ctor
        private readonly IMemoryCache _memoryCache;
        private readonly IPhoneTotpProvider _totp;
        private readonly TotpSettings _options;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _env;
        private readonly ISmsSender _smsSender;
        private readonly UserManager<ApplicationUser> _userManager;

        public TotpCode(IMemoryCache memoryCache, IPhoneTotpProvider totp, IOptions<TotpSettings> options, IEmailSender emailSender, IWebHostEnvironment env, ISmsSender smsSender, UserManager<ApplicationUser> userManager)
        {
            _memoryCache = memoryCache;
            _totp = totp;
            _emailSender = emailSender;
            _env = env;
            _smsSender = smsSender;
            _userManager = userManager;
            _options = options?.Value ?? new TotpSettings();
        }



        #endregion

        public async Task<PhoneTotpResult> SendTotpCodeAsync(string phoneNo,string sendType)
        {
            //آیا کد ریست قبلا ارسال شده است
            var isResetPasswordTotp =
                _memoryCache.TryGetValue(phoneNo + sendType, out TotpTempData resetPasswordTotp);
            if (isResetPasswordTotp)
            {
                //  اگر هنوز کد ارسالی معتبر بود
                //نیاز به این شرط نیست چون اگر زمان منقضی شده باشد خود به خود از کش پاک می شود
                if (DateTime.Now < resetPasswordTotp.ExpirationTime)
                {
                    var remainTime = (int)(resetPasswordTotp.ExpirationTime - DateTime.Now).TotalSeconds; ;
                    return new PhoneTotpResult(false, $"ارسال کد جدید {remainTime.ToString()} ثانیه ");
                }

            }
            

            var secretKey = _totp.CreateSecretKey();
            var totpCode = _totp.GenerateTotp(secretKey);

            var totpTemp = new TotpTempData()
            {
                SecretKey = secretKey,
                UserMobileNo = phoneNo,
                ExpirationTime = DateTime.Now.AddSeconds(_options.Step)
            };
            // var memoryCacheOptions = new MemoryCacheEntryOptions()
            //    .SetSlidingExpiration(TimeSpan.FromSeconds(_options.Step));
            _memoryCache.Set(phoneNo + sendType, totpTemp,
                TimeSpan.FromSeconds(_options.Step));
            //جهت تست
            if (_env.IsDevelopment())
            {
                await _emailSender.SendEmailAsync("test@test.com", "sms to " + phoneNo, totpCode, false);
                return new PhoneTotpResult(true, "");
            }

            await _smsSender.SendSmsAsync(phoneNo, totpCode, "شرکت فلان");
            return new PhoneTotpResult(true, "");
        }

        public async Task<PhoneTotpResult> ConfirmTotpCodeAsync(string phoneNo ,string code,string confirmType)
        {
            var isResetPasswordTotp =
                _memoryCache.TryGetValue(phoneNo + confirmType, out TotpTempData resetPasswordTotp);
        
            if (!isResetPasswordTotp ||(DateTime.Now > resetPasswordTotp.ExpirationTime))
            {
                //  اگر هنوز کد ارسالی معتبر بود
                //نیاز به این شرط نیست چون اگر زمان منقضی شده باشد خود به خود از کش پاک می شود
            

                    return new PhoneTotpResult(false, ErrorMessageRes.TotpCodeExpire);
               

            }
           
            var mathResult = _totp.VerifyTotp(resetPasswordTotp.SecretKey, code);
            //اگر کد درست باشد
            if (mathResult.Successed)
            {
  
                return new PhoneTotpResult(true, "");

            }

            // اگر کد غلط باشد

            return new PhoneTotpResult(false, ErrorMessageRes.WrongTotpInput);
        }


    }
}
