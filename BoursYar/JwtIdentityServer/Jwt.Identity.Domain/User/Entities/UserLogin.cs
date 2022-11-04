using System;

namespace Jwt.Identity.Domain.User.Entities
{
    public class UserLogin
    {
        public string UserId { get; set; }

        public string AccessToken { get; set; }

        public string Device { get; set; }

        public string IpAdress { get; set; }

        public DateTime LoginTime { get; set; } = DateTime.Now;
    }
}