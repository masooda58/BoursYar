using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Jwt.Identity.Domain.IServices.ITokenServices;
using Jwt.Identity.Domain.IServices.ITokenServices.TokenModels;

namespace Jwt.Identity.Api.TokenServices
{
    public class TokenGenrators:ITokenGenrators
    {
        public UserTokenModel GetAccessToken(List<Claim> authClaims)
        {
            throw new NotImplementedException();
        }

        public TokenModel GetRefreshToken()
        {
            throw new NotImplementedException();
        }
    }
}
