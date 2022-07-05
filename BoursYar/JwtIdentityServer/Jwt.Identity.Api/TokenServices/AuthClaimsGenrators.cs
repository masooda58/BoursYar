using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Jwt.Identity.Domain.IServices.ITokenServices;
using Jwt.Identity.Domain.Models;

namespace Jwt.Identity.Api.TokenServices
{
    public class AuthClaimsGenrators:IAuthClaimsGenrators
    {
        public Task<List<Claim>> CreatClaims(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
