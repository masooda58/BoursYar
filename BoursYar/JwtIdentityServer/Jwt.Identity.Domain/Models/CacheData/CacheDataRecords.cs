using System;
using Jwt.Identity.Domain.Models.TypeEnum;

namespace Jwt.Identity.Domain.Models.CacheData
{
    public record TempConfirmTotp(string PhoneNo, TotpTypeCode TypeTotp);
    public record TempConfirmEmail(string PhoneNo, TotpTypeCode TypeTotp);
    public record TotpTempData(string UserMobileNo, DateTime ExpirationTime, byte[] SecretKey);

    public record TempIpBlock(string IpAddress, DateTime ExpirationTime);
}
