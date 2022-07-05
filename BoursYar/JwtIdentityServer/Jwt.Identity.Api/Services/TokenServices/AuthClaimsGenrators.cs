using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Interfaces.ITokenServices;
using Jwt.Identity.Domain.IServices.ITokenServices;
using Jwt.Identity.Domain.Models;

namespace Jwt.Identity.Api.Services.TokenServices
{
    public class AuthClaimsGenrators:IAuthClaimsGenrators
    {
        public Task<List<Claim>> CreatClaims(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
