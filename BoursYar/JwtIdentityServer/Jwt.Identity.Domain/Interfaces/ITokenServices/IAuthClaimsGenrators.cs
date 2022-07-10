using Jwt.Identity.Domain.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Jwt.Identity.Domain.Interfaces.ITokenServices
{
    public interface IAuthClaimsGenrators
    {
        // های که در یوزر هست رابصورت یک لیست تهیه می کند Claim
        Task<List<Claim>> CreatClaims(ApplicationUser user);
    }
}
