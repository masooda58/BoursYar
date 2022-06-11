using System;

namespace IdentityApi.Models.Response
{
    public class UserTokenResponse
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
