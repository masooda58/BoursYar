using BoursYarAuthorization.Handler;
using BoursYarAuthorization.Requirement;
using BoursYarAuthorization.Utilities;
using BoursYarAuthorization.Utilities.MvcNameUtilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BoursYarAuthorization.IOC
{
    public static class DependancyInjection
    {
        public static void AddBoursYarAuthorize(this IServiceCollection service)
        {

            service.AddSingleton<IClaimBaseAuthorizationUtilities, ClaimBaseAuthorizationUtilities>();
            service.AddSingleton<IMvcUtilities, MvcUtilities>();
            service.AddScoped<IAuthorizationHandler, ClaimBaseHandler>();
            service.AddHttpContextAccessor();
           // استفاده می کنیم AuthorizeCore آز AddAuthorize هستیم بجای Class Liberary چون در
            
            service.AddAuthorizationCore(option =>
            {
                option.AddPolicy("ClaimBaseAuthorization", policy =>
                    policy.Requirements.Add(new ClaimBaseRequirement())
                );
            });

        }
    }
}
