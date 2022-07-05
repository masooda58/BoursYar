using System;
using Jwt.Identity.Domain.Interfaces.ITokenServices;
using Jwt.Identity.Domain.IServices.ITokenServices;

namespace Jwt.Identity.Api.Services.TokenServices
{
    public class TokenValidators:ITokenValidators
    {
        public bool Validate(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
