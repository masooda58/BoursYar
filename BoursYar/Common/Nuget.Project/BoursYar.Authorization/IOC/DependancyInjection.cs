using BoursYar.Authorization.Handler;
using BoursYar.Authorization.Requirement;
using BoursYar.Authorization.Utilities;
using BoursYar.Authorization.Utilities.MvcNameUtilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BoursYar.Authorization.IOC
{
    /// <summary>
    ///  می کند Inject های مورد نیاز را Dependancy
    /// </summary>
    
    public static class DependancyInjection
    {
        /// <summary>
        /// // اضافه می شود Startup.cs توسط این تابع کل سرویسهای مورد نیاز بهServices در فایل
        /// </summary>
        /// <param name="service"> یک نمونه از IServiceCollection</param>
        /// <remarks>
        ///  میسازد BoursYarAuthorization به نام Authorization policy یک
        /// <code>
        /// service.AddAuthorizationCore(option =>{
        /// option.AddPolicy("BoursYarAuthorization", policy =>
        /// policy.Requirements.Add(new ClaimBaseRequirement()));
        /// });
        /// </code>
        /// می شوند Inject موارد زیر نیز
        /// <code>
        /// service.AddSingleton&lt;IClaimBaseAuthorizationUtilities, ClaimBaseAuthorizationUtilities&gt;();
        /// service.AddSingleton&lt;IMvcUtilities, MvcUtilities&gt;();
        /// service.AddScoped&lt;IAuthorizationHandler, ClaimBaseHandler&gt;();
        /// service.AddHttpContextAccessor();
        /// </code>
        /// </remarks>
        ///<example>
        /// <code>
        /// services.AddBoursYarAuthorize();
        /// </code>
        /// </example>
        public static void AddBoursYarAuthorize(this IServiceCollection service)
        {

            service.AddSingleton<IClaimBaseAuthorizationUtilities, ClaimBaseAuthorizationUtilities>();
            service.AddSingleton<IMvcUtilities, MvcUtilities>();
            service.AddScoped<IAuthorizationHandler, ClaimBaseHandler>();
            service.AddHttpContextAccessor();
            // استفاده می کنیم AuthorizeCore آز AddAuthorize هستیم بجای Class Liberary چون در

            service.AddAuthorizationCore(option =>
            {
                option.AddPolicy("BoursYarAuthorization", policy =>
                    policy.Requirements.Add(new ClaimBaseRequirement())
                );
            });

        }
    }
}
