using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Jwt.Identity.BoursYarServer.SettingModels;
using Jwt.Identity.Domain.Interfaces.IPhoneTotpProvider;
using Jwt.Identity.Domain.Models;
using Microsoft.Extensions.Options;
using OtpNet;

namespace Jwt.Identity.BoursYarServer.Services.PhoneTotpProvider
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
        public PhoneTotpResult VerifyTotp(byte[] secretKey, string code)
        {
            CreateTotp(secretKey);
            var isTotpValid = _totp.VerifyTotp(code, out _,VerificationWindow.RfcSpecifiedNetworkDelay);
           return isTotpValid ? 
               new PhoneTotpResult(true, null)
               : new PhoneTotpResult(false, "کد وراد شده معتبر نیست لطفا کد جدید دریافت نمایید");
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
