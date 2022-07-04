//نحوه استفاده در زیر کد
// نیاز به این پکیج است Microsoft.Aspnet.Core.Cors
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.Dependency.Cors
{
    public static class CorsExtension
    {

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
                                .AllowAnyHeader();
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

#region AppSettingCors
//"Cors": {
//"Origin": [ "Https://localhost:3000", "Https://localhost:3001" ],
//"Method": [ "OPTIONS","GET","HEAD","POST","PUT","DELETE" ]
//},
#endregion

#region ServiceConfig
//var corsOrigin = Configuration.GetSection("Cors:Origin").Get<string[]>();
//var corsMethod = Configuration.GetSection("Cors:Method").Get<string[]>();
//services.AddOurCors(corsOrigin, corsMethod);
#endregion

