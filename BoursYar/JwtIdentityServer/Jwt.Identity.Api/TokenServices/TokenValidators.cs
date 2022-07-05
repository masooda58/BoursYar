using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Jwt.Identity.Domain.IServices.ITokenServices;

namespace Jwt.Identity.Api.TokenServices
{
    public class TokenValidators:ITokenValidators
    {
        public bool Validate(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
