using System;
using System.Collections.Generic;
using System.Security.Claims;
using Jwt.Identity.Domain.Interfaces.ITokenServices;
using Jwt.Identity.Domain.IServices.ITokenServices;
using Jwt.Identity.Domain.IServices.ITokenServices.TokenModels;

namespace Jwt.Identity.Api.Services.TokenServices
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
