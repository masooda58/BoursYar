﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jwt.Identity.BoursYarServer.OptionsModels;
using Jwt.Identity.Domain.Interfaces.IPhoneTotpProvider;
using Jwt.Identity.Domain.Models;
using Microsoft.Extensions.Options;
using OtpNet;

namespace Jwt.Identity.BoursYarServer.Services.PhoneTotpProvider
{
    public class PhoneTotpProvider:IPhoneTotpProvider
    {
        private readonly TotpOptions _options;
        private Totp _totp;

        public PhoneTotpProvider(IOptions<TotpOptions> options)
        {
            _options = options?.Value??new TotpOptions();
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
            var isTotpValid = _totp.VerifyTotp(code, out _);
           return isTotpValid ? 
               new PhoneTotpResult(true, null)
               : new PhoneTotpResult(false, "کد وراد شده معتبر نیست لطفا کد جدید دریافت نمایید");
        }
       
        private void CreateTotp(byte[] secretKey)
        {

            _totp = new Totp(secretKey,step:_options.Step);
        }
    }
}