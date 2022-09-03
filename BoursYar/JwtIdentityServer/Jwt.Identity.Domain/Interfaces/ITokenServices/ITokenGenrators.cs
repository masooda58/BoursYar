using System.Collections.Generic;
using System.Security.Claims;
using Jwt.Identity.Domain.Models.ResultModels.TokenModels;

namespace Jwt.Identity.Domain.Interfaces.ITokenServices
{
    public interface ITokenGenrators
    {
        public UserTokenModel GetAccessToken(List<Claim> authClaims);
        public TokenModel GetRefreshToken();
    }
}
