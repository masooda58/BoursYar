using Jwt.Identity.Domain.Models;

namespace Jwt.Identity.Domain.Interfaces.IPhoneTotpProvider
{
    public interface IPhoneTotpProvider
    {
        /// <summary>
        /// // Totp ساخت
        /// </summary>
        /// <param name="secretKey"></param>
        /// <returns> code Totp</returns>
        public string GenerateTotp(byte[] secretKey);
        /// <summary>
        /// تایید کد دریافتی
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="code"></param>
        /// <returns>  نتیجه تایید کد</returns>
        public PhoneTotpResult VerifyTotp(byte[] secretKey, string code);
    }
}
