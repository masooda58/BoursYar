using Microsoft.AspNetCore.Http;

namespace BoursYar.Authorization.Utilities
{
    public interface IClaimBaseAuthorizationUtilities
    {
        //  مقدار 1 را از مسیر می گیرد
        //1 BoursyarAuthorize from url
        string GetClaimToAuthorize(HttpContext httpContext);
    }
}
