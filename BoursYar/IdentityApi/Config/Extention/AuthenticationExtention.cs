using System;
using IdentityApi.Config.Extention.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace IdentityApi.Config.Extention
{
    public static class AuthenticationExtention
    {

        public static IServiceCollection AddOurAuthentication(this IServiceCollection services, JwtSettingModel jwtSetting)
        {
            services.AddAuthentication(options =>
                {
                   //options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
            
                    // Adding Jwt Bearer
                        .AddJwtBearer(options =>
                        {
                            options.SaveToken = true;
                            options.RequireHttpsMetadata = false;
                            options.TokenValidationParameters = new TokenValidationParameters()
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidAudience = jwtSetting.ValidAudience,
                                ValidIssuer = jwtSetting.ValidIssuer,
                                RequireExpirationTime = true,
                                ClockSkew = TimeSpan.Zero,

                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret))
                            };
                        });
                
            return services;
        }
    }
}
