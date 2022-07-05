using System;

namespace Jwt.Identity.Domain.IServices.ITokenServices.TokenModels
{
    public class UserTokenModel:TokenModel
    {
     
        public string RefreshToken { get; set; }
   
        public DateTime RefreshExpiration { get; set; }
    }
}