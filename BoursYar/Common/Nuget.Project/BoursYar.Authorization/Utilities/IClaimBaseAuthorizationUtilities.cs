using Microsoft.AspNetCore.Http;

namespace BoursYar.Authorization.Utilities
{

    /// <summary>
    /// به اینجا مراجعه شود <see cref="ClaimBaseAuthorizationUtilities"/>
    /// </summary>
    public interface IClaimBaseAuthorizationUtilities
    {
        //  مقدار 1 را از مسیر می گیرد
        //1 BoursyarAuthorize from url
        /// <summary>
        ///  داده شده را استخراج می کند Rout موزد نیاز claim
        /// </summary>
        /// <param name="httpContext">httpContext </param>
        /// <returns> MvcModelName</returns>
        string GetClaimToAuthorize(HttpContext httpContext);
    }
}
