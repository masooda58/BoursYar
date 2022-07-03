using Microsoft.AspNetCore.Http;

namespace BoursYarAuthorization.Utilities
{
    public interface IClaimBaseAuthorizationUtilities
    {
        //  مقدار 1 را از مسیر می گیرد
        //1:ClaimToAuthorize from url
        string GetClaimToAuthorize(HttpContext httpContext);
    }
}
