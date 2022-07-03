

using IdentityApi.Authoriz.ClaimBaseAuthoriz.Handler;
using IdentityApi.Authoriz.ClaimBaseAuthoriz.Requirement;
using IdentityApi.Authoriz.ClaimBaseAuthoriz.Utilities;
using IdentityApi.Authoriz.ClaimBaseAuthoriz.Utilities.MvcNameUtilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityApi.Authoriz.ClaimBaseAuthoriz
{
    public static class DependancyInjection
    {
        public static void AddClaimBaseAuthorize(this IServiceCollection service)
        {

            service.AddSingleton<IClaimBaseAuthorizationUtilities, ClaimBaseAuthorizationUtilities>();
            service.AddSingleton<IMvcUtilities, MvcUtilities>();
            service.AddScoped<IAuthorizationHandler, ClaimBaseHandler>();
            service.AddAuthorization(option =>
            {
                option.AddPolicy("ClaimBaseAuthorization", policy =>
                    policy.Requirements.Add(new ClaimBaseRequirement())
                );
            });

        }
    }
}
