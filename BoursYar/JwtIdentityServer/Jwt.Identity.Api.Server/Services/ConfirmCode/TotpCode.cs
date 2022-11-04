﻿using System;
using System.Threading.Tasks;
using Jwt.Identity.Api.Server.Resources;
using Jwt.Identity.Domain.IServices;
using Jwt.Identity.Domain.IServices.Totp;
using Jwt.Identity.Domain.IServices.Totp.Enum;
using Jwt.Identity.Domain.IServices.Totp.SettingModels;
using Jwt.Identity.Domain.Shared.Models.CacheData;
using Jwt.Identity.Domain.User.Entities;
using Jwt.Identity.Framework.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Jwt.Identity.Api.Server.Services.ConfirmCode
{
    public class TotpCode : ITotpCode
    {
        public async Task<ResultResponse> SendTotpCodeAsync(string phoneNo, TotpTypeCode sendType)
        {
            #region Ip Block for send sms

            var remoteIpAddress = _httpContextAccessor.HttpContext!.Connection.RemoteIpAddress;
            var isIpBlock = _memoryCache.TryGetValue(remoteIpAddress!.ToString(), out TempIpBlock ipBlock);

            #endregion

            //آیا کد قبلا ارسال شده است
            var isPasswordTotp =
                _memoryCache.TryGetValue(phoneNo + sendType, out TotpTempData passwordTotp);
            if (isPasswordTotp)
                //  اگر هنوز کد ارسالی معتبر بود
                //نیاز به این شرط نیست چون اگر زمان منقضی شده باشد خود به خود از کش پاک می شود
                if (DateTime.Now < passwordTotp.ExpirationTime)
                {
                    var remainTime = (int)(passwordTotp.ExpirationTime - DateTime.Now).TotalSeconds;
                    return new ResultResponse(false, $"{remainTime}");
                }

            if (isIpBlock)
            {
                var remainTime = (int)(ipBlock.ExpirationTime - DateTime.Now).TotalSeconds;
                return new ResultResponse(false, $"{remainTime}");
            }


            var secretKey = _totp.CreateSecretKey();
            var totpCode = _totp.GenerateTotp(secretKey);

            var totpTemp = new TotpTempData(SecretKey: secretKey, UserMobileNo: phoneNo,
                ExpirationTime: DateTime.Now.AddSeconds(_options.Step));
            var tempIpBlock = new TempIpBlock(remoteIpAddress!.ToString(),
                DateTime.Now.AddSeconds(_options.Step - 2));
            // var memoryCacheOptions = new MemoryCacheEntryOptions()
            //    .SetSlidingExpiration(TimeSpan.FromSeconds(_options.Step));
            _memoryCache.Set(phoneNo + sendType, totpTemp,
                TimeSpan.FromSeconds(_options.Step));
            // block ip for send sms in step time
            _memoryCache.Set(remoteIpAddress!.ToString(), tempIpBlock,
                TimeSpan.FromSeconds(_options.Step));

            //جهت تست
            if (_env.IsDevelopment())
            {
                await _emailSender.SendEmailAsync("test@test.com", "sms to " + phoneNo, totpCode);
                return new ResultResponse(true, MessageRes.TotpSent);
            }

            await _smsSender.SendSmsAsync(phoneNo, totpCode, "شرکت فلان");
            return new ResultResponse(true, MessageRes.TotpSent);
        }

        public async Task<ResultResponse> ConfirmTotpCodeAsync(string phoneNo, string code, TotpTypeCode confirmType)
        {
            var isResetPasswordTotp =
                _memoryCache.TryGetValue(phoneNo + confirmType, out TotpTempData PasswordTotp);

            if (!isResetPasswordTotp || DateTime.Now > PasswordTotp.ExpirationTime)
                //  اگر هنوز کد ارسالی معتبر بود
                //نیاز به این شرط نیست چون اگر زمان منقضی شده باشد خود به خود از کش پاک می شود

                return new ResultResponse(false, MessageRes.CodeExpire);

            var mathResult = _totp.VerifyTotp(PasswordTotp.SecretKey, code);
            //اگر کد درست باشد
            if (mathResult.Successed)
            {
                var remoteIpAddress = _httpContextAccessor.HttpContext!.Connection.RemoteIpAddress;

                _memoryCache.Remove(remoteIpAddress!.ToString());
                _memoryCache.Remove(phoneNo + confirmType);

                return new ResultResponse(true, "");
            }

            // اگر کد غلط باشد

            return new ResultResponse(false, MessageRes.WrongTotpInput);
        }

        #region Ctor

        private readonly IMemoryCache _memoryCache;
        private readonly IPhoneTotpProvider _totp;
        private readonly TotpSettings _options;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _env;
        private readonly ISmsSender _smsSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public TotpCode(IMemoryCache memoryCache, IPhoneTotpProvider totp, IOptions<TotpSettings> options,
            IEmailSender emailSender, IWebHostEnvironment env, ISmsSender smsSender,
            UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _memoryCache = memoryCache;
            _totp = totp;
            _emailSender = emailSender;
            _env = env;
            _smsSender = smsSender;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;


            _options = options?.Value ?? new TotpSettings();
        }

        #endregion
    }
}