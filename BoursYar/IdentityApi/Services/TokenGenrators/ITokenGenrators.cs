using IdentityApi.Models.Response;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityApi.Services.TokenGenrators
{
    public interface ITokenGenrators
    {
        public UserTokenResponse GetAccessToken(List<Claim> authClaims);
        public TokenModel GetRefreshToken();
    }
}
