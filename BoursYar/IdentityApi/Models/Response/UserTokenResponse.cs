using System;

namespace IdentityApi.Models.Response
{
    public class UserTokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime RefreshExpiration { get; set; }

    }
}
