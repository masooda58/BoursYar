using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityApi.Models.Response;

namespace IdentityApi.Services.TokenGenrators
{
    public interface ITokenGenrators
    {
        public UserTokenResponse GetAccessToken(List<Claim> authClaims);
        public TokenModel GetRefreshToken();
    }
}
