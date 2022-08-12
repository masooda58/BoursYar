//نحوه استفاده در زیر کد
// نیاز به این پکیج است Microsoft.Aspnet.Core.Cors
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.Dependency.Cors
{
    /// <summary>
    ///  CorsExtension 
    /// </summary>
    public static class CorsExtension
    {
        /// <summary>
        /// // های مورد نیاز Cors اضافه کردن 
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="corsOrigin"> string[] corsOrigin</param>
        /// <param name="corsMethod">string[] corsMethod</param>
        /// <example>
        /// in AppSetting.Json file
        /// <code>
        /// "Cors": {
        /// "Origin": [ "Https://localhost:3000", "Https://localhost:3001" ],
        /// "Method": [ "OPTIONS","GET","HEAD","POST","PUT","DELETE" ]
        /// },
        /// </code>
        /// i serviceConfig StartUp.cs file
        /// var corsOrigin = Configuration.GetSection("Cors:Origin").Get&lt;string[]&gt;();
        /// var corsMethod = Configuration.GetSection("Cors:Method").Get&lt;string[]&gt;();
        /// services.AddOurCors(corsOrigin, corsMethod);
        /// </example>
        /// <returns> service </returns>

        public static IServiceCollection AddOurCors(this IServiceCollection services,
            string[] corsOrigin, string[] corsMethod)
        {
            if (corsOrigin.Length == 0 && corsMethod.Length == 0)
            {
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder
                                .AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials();
                        });
                });
                return services;

            }
            if (corsOrigin.Length > 0 && corsMethod.Length == 0)
            {
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder
                                .WithOrigins(corsOrigin)
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                        });
                });
                return services;

            }
            if (corsOrigin.Length > 0 && corsMethod.Length > 0)
            {
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder
                                .WithOrigins(corsOrigin)
                                .WithMethods(corsMethod)
                                .AllowAnyHeader();
                        });
                });
                return services;

            }
            if (corsOrigin.Length == 0 && corsMethod.Length > 0)
            {
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder
                                .AllowAnyOrigin()
                                .WithMethods(corsMethod)
                                .AllowAnyHeader();
                        });
                });
                return services;

            }
            return services;
        }
    }
}
//نحوه استفاده

//#region AppSettingCors
////"Cors": {
////"Origin": [ "Https://localhost:3000", "Https://localhost:3001" ],
////"Method": [ "OPTIONS","GET","HEAD","POST","PUT","DELETE" ]
////},
//#endregion

//#region ServiceConfig
////var corsOrigin = Configuration.GetSection("Cors:Origin").Get<string[]>();
////var corsMethod = Configuration.GetSection("Cors:Method").Get<string[]>();
////services.AddOurCors(corsOrigin, corsMethod);
//#endregion

