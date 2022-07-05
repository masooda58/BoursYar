using System;

namespace Jwt.Identity.Domain.IServices.ITokenServices.TokenModels
{
    public class TokenModel
    {
      
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }

       
    }
}