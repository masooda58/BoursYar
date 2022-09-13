﻿using System.Security.Cryptography;
using Jwt.Identity.Domain.Interfaces.IPhoneTotpProvider;
using Jwt.Identity.Domain.Models.ResultModels;
using Jwt.Identity.Domain.Models.SettingModels;
using Microsoft.Extensions.Options;
using OtpNet;

namespace Jwt.Identity.Api.Server.Services.PhoneTotpProvider
{
    public class PhoneTotpProvider:IPhoneTotpProvider
    {
        private readonly TotpSettings _options;
        private Totp _totp;

        public PhoneTotpProvider(IOptions<TotpSettings> options)
        {
            _options = options?.Value??new TotpSettings();
        }
 ///  <inheritdoc />
        public string GenerateTotp(byte[] secretKey)
        {
            CreateTotp(secretKey);
            return _totp.ComputeTotp();
        }
 ///  <inheritdoc />
        public ConfirmResult VerifyTotp(byte[] secretKey, string code)
        {
            CreateTotp(secretKey);
            var isTotpValid = _totp.VerifyTotp(code, out _,VerificationWindow.RfcSpecifiedNetworkDelay);
           return isTotpValid ? 
               new ConfirmResult(true, null)
               : new ConfirmResult(false, "کد وراد شده معتبر نیست لطفا کد جدید دریافت نمایید");
        }
       
        private void CreateTotp(byte[] secretKey)
        {

            _totp = new Totp(secretKey,step:_options.Step);
        }
        ///  <inheritdoc />
        public byte[] CreateSecretKey()
        {
            using var rng =new RNGCryptoServiceProvider();
            var secretKey = new byte[32];
            rng.GetBytes(secretKey);
            return secretKey;
        }
    }
}
