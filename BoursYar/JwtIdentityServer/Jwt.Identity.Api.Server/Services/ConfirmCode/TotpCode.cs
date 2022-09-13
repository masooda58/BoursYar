using System;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Interfaces.IConfirmCode;
using Jwt.Identity.Domain.Interfaces.IMessageSender;
using Jwt.Identity.Domain.Interfaces.IPhoneTotpProvider;
using Jwt.Identity.Domain.Models;
using Jwt.Identity.Domain.Models.CacheData;
using Jwt.Identity.Domain.Models.ResultModels;
using Jwt.Identity.Domain.Models.TypeCode;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Jwt.Identity.Api.Server.Services.ConfirmCode
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

        public TotpCode(IMemoryCache memoryCache, IPhoneTotpProvider totp, IOptions<TotpSettings> options, IEmailSender emailSender, IWebHostEnvironment env, ISmsSender smsSender, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _memoryCache = memoryCache;
            _totp = totp;
            _emailSender = emailSender;
            _env = env;
            _smsSender = smsSender;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
            ;
            _options = options?.Value ?? new TotpSettings();
        }



        #endregion

        public async Task<ConfirmResult> SendTotpCodeAsync(string phoneNo, TotpTypeCode sendType)
        {
            #region Ip Block for send sms
          
            var remoteIpAddress = _httpContextAccessor.HttpContext!.Connection.RemoteIpAddress;
            var isIpBlock= _memoryCache.TryGetValue(remoteIpAddress!.ToString(), out TempIpBlock ipBlock);
            

            #endregion
   
            //آیا کد ریست قبلا ارسال شده است
            var isResetPasswordTotp =
                _memoryCache.TryGetValue(phoneNo + sendType, out TotpTempData resetPasswordTotp);
            if (isResetPasswordTotp)
            {
                //  اگر هنوز کد ارسالی معتبر بود
                //نیاز به این شرط نیست چون اگر زمان منقضی شده باشد خود به خود از کش پاک می شود
                if (DateTime.Now < resetPasswordTotp.ExpirationTime)
                {
                    var remainTime = (int)(resetPasswordTotp.ExpirationTime - DateTime.Now).TotalSeconds; 
                    return new ConfirmResult(false, ErrorMessage: $"{ remainTime }");
                }

            }

            if (isIpBlock)
            {
                var remainTime = (int)(ipBlock.ExpirationTime - DateTime.Now).TotalSeconds; 
                return new ConfirmResult(false, ErrorMessage: $"{ remainTime }");

            }
            

            var secretKey = _totp.CreateSecretKey();
            var totpCode = _totp.GenerateTotp(secretKey);

            var totpTemp = new TotpTempData()
            {
                SecretKey = secretKey,
                UserMobileNo = phoneNo,
                ExpirationTime = DateTime.Now.AddSeconds(_options.Step)
            };
            var tempIpBlock = new TempIpBlock()
            {
                IpAddress = remoteIpAddress!.ToString(),
                ExpirationTime = DateTime.Now.AddSeconds(_options.Step-2)
            };
            // var memoryCacheOptions = new MemoryCacheEntryOptions()
            //    .SetSlidingExpiration(TimeSpan.FromSeconds(_options.Step));
            _memoryCache.Set(phoneNo + sendType, totpTemp,
                TimeSpan.FromSeconds(_options.Step));
            // block ip for send sms in step time
            _memoryCache.Set(remoteIpAddress!.ToString(), tempIpBlock,
                TimeSpan.FromSeconds(_options.Step));
            var tempData = _tempDataDictionaryFactory.GetTempData(_httpContextAccessor.HttpContext);
            tempData["PhoneNo"] = phoneNo;
            //جهت تست
            if (_env.IsDevelopment())
            {
                await _emailSender.SendEmailAsync("test@test.com", "sms to " + phoneNo, totpCode, false);
                return new ConfirmResult(true, "");
            }

            await _smsSender.SendSmsAsync(phoneNo, totpCode, "شرکت فلان");
            return new ConfirmResult(true, "");
        }

        public async Task<ConfirmResult> ConfirmTotpCodeAsync(string phoneNo ,string code,TotpTypeCode confirmType)
        {
            var isResetPasswordTotp =
                _memoryCache.TryGetValue(phoneNo + confirmType, out TotpTempData resetPasswordTotp);
        
            if (!isResetPasswordTotp ||(DateTime.Now > resetPasswordTotp.ExpirationTime))
            {
                //  اگر هنوز کد ارسالی معتبر بود
                //نیاز به این شرط نیست چون اگر زمان منقضی شده باشد خود به خود از کش پاک می شود
            

                    return new ConfirmResult(false, ErrorMessageRes.CodeExpire);
               

            }
           
            var mathResult = _totp.VerifyTotp(resetPasswordTotp.SecretKey, code);
            //اگر کد درست باشد
            if (mathResult.Successed)
            {
  
                return new ConfirmResult(true, "");

            }

            // اگر کد غلط باشد

            return new ConfirmResult(false, ErrorMessageRes.WrongTotpInput);
        }


    }
}
