using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IdentityApi.Authoriz.ClaimBaseAuthoriz.Utilities
{
    public interface IClaimBaseAuthorizationUtilities
    {
        string GetClaimToAuthorize(HttpContext httpContext);
    }
}
