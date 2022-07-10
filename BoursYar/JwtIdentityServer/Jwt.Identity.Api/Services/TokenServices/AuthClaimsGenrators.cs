using BoursYar.Authorization.repositories;
using Jwt.Identity.Domain.Interfaces.ITokenServices;
using Jwt.Identity.Domain.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Jwt.Identity.Api.Services.TokenServices
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
                new Claim(ClaimStore.BoursYarAccess,"x"),
                new Claim(ClaimStore.BoursYarAccess,"y"),
                new Claim("id","user.Id"),

            };
            return authClaims;
        }
    }
}
