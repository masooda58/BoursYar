using System;

namespace Jwt.Identity.Domain.Models.CacheData
{
   public class TempIpBlock
    {
        public string IpAddress { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
