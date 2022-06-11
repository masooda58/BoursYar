using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityApi.Config.Extention
{
    public static class CorsExtention
    {
        public static IServiceCollection AddOurCors(this IServiceCollection services,
            string[] corsOrigin,string[] CorsMethod)
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
                options.AddPolicy("MyCORSPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:49373")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            return services;
        }
    }
}
