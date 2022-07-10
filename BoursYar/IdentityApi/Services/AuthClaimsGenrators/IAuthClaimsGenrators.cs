using IdentityApi.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityApi.Services.AuthClaimsGenrators
{
    public interface IAuthClaimsGenrators
    {
        public Task<List<Claim>> CreatClaims(ApplicationUser user);
    }
}
