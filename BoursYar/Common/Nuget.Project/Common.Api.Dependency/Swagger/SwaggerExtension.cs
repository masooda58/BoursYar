//نحوه استفاده در ذیل کد
// پکیج مورد استفاده  <PackageReference Include="Swashbuckle.AspNetCore" Version="5.6.3" />
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace Common.Api.Dependency.Swagger
{
    /// <summary>
    /// SwaggerExtension with Authorize
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// Swagger تنظیمات
        /// </summary>
        /// <remarks>
        /// استفاده می شود Extension از این Authorize با کلید Swagger جهت استفاده از
        /// </remarks>
        /// <example>
        /// نحوه استفاده
        /// <code>
        /// services.AddOurSwagger();
        /// </code>
        /// </example>
        /// <param name="services">services</param>
        /// <returns>services</returns>
        public static IServiceCollection AddOurSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = " API",
                    Description = "just for test api",


                    License = new OpenApiLicense
                    {
                        Name = "Use under Boursyar",

                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

            });
            return services;
        }
    }
}

//#region How use it

////services.AddOurSwagger();


//#endregion
