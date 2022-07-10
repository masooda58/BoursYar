using Jwt.Identity.Domain.Models.TokenModels;
using System.Collections.Generic;
using System.Security.Claims;

namespace Jwt.Identity.Domain.Interfaces.ITokenServices
{
    public interface ITokenGenrators
    {
        public UserTokenModel GetAccessToken(List<Claim> authClaims);
        public TokenModel GetRefreshToken();
    }
}
