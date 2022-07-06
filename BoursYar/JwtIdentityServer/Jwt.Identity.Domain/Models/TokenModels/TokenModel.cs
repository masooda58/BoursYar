using System;

namespace Jwt.Identity.Domain.Models.TokenModels
{
    public class TokenModel
    {
      
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }

       
    }
}