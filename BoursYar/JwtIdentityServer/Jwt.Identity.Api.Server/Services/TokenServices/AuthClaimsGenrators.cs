using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Interfaces.ITokenServices;
using Jwt.Identity.Domain.Models;

namespace Jwt.Identity.Api.Server.Services.TokenServices
{
    // این کلاس یک پیاده سازی موقت است تا تعیین نحوه اصلی دسترسی در توکن
    // این کلاس تعیین می کنن چه مواردی در توکن اصلی قرار بگیرد
    public class AuthClaimsGenrators : IAuthClaimsGenrators
    {
        public async Task<List<Claim>> CreatClaims(ApplicationUser user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "user.UserName"),
                new Claim("BoursYarAccess","x"),
                new Claim("BoursYarAccess","y"),
                new Claim("id","user.Id"),

            };
            return authClaims;
        }
    }
}
