using System;
using Jwt.Identity.Domain.Models.ResultModels.TokenModels;

namespace Jwt.Identity.Domain.Models.Response
{
    public class UserTokenResponse : TokenModel
    {

        public string RefreshToken { get; set; }

        public DateTime RefreshExpiration { get; set; }
    }
}