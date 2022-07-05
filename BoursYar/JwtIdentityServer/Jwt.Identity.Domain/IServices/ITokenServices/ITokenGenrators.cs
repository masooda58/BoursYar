using System.Collections.Generic;
using System.Security.Claims;
using Jwt.Identity.Domain.IServices.ITokenServices.TokenModels;

namespace Jwt.Identity.Domain.IServices.ITokenServices
{
    public interface ITokenGenrators
    {
        public UserTokenModel GetAccessToken(List<Claim> authClaims);
        public TokenModel GetRefreshToken();
    }
}
