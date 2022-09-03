using System;

namespace Jwt.Identity.Domain.Models.TransferData
{
    public class TotpTempData

    {
        public byte[] SecretKey { get; set; }
        public string UserMobileNo { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
