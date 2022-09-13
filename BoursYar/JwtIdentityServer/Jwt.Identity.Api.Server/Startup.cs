using Common.Api.Dependency.Cors;
using Common.Api.Dependency.Swagger;
using Common.Jwt.Authentication;
using IdentityPersianHelper.Identity;
using Jwt.Identity.Api.Server.Helpers.CustomSignIn;
using Jwt.Identity.Api.Server.Security;
using Jwt.Identity.Data.Context;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Jwt.Identity.Domain.Models.SettingModels;

namespace Jwt.Identity.Api.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region DbContext
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("IdetityDb")), ServiceLifetime.Transient);

            #endregion

            #region Identity
                services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                 {
                     // Password settings
                     options.Password.RequireDigit = false;
                     options.Password.RequiredLength = 1;
                     options.Password.RequireNonAlphanumeric = false;
                     options.Password.RequireUppercase = false;
                     options.Password.RequireLowercase = false;
                     options.Password.RequiredUniqueChars = 1;

                     // Lockout settings
                     options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                     options.Lockout.MaxFailedAccessAttempts = 3;
                     options.Lockout.AllowedForNewUsers = true;

                     // User settings
                     options.User.RequireUniqueEmail = false;

                     // SignIn settings
                     //options.SignIn.RequireConfirmedEmail = false;
                     // options.SignIn.RequireConfirmedPhoneNumber = false;
                     options.SignIn.RequireConfirmedAccount = true;

                     //Email token provider
                     options.Tokens.EmailConfirmationTokenProvider = "EmailConFirmation";
                 })
                    .AddEntityFrameworkStores<IdentityContext>()
                    .AddDefaultTokenProviders()
                    //email Token Provider
                    .AddTokenProvider<EmailConfirmationTokenProvide<ApplicationUser>>("EmailConFirmation")
                    .AddSignInManager<CustomSignInManager>() //custom signin manage for mobile or email
                    .AddErrorDescriber<PersianIdentityErrorDescriber>();

                // تغییر زمان اعتبار همه توکن های ساخته شده
                services.Configure<DataProtectionTokenProviderOptions>(o =>
                {
                     o.TokenLifespan = TimeSpan.FromHours(8);
                });
                //تغییر زمان توکن ایمیل
                services.Configure<EmailConfirmationTokenProviderOptions>(o =>
                {
                o.TokenLifespan = TimeSpan.FromHours(2);
                });
            #endregion

            #region MemoryCache

            services.AddMemoryCache();


            #endregion

            #region Options from AppSetting

            #region Totp

            services.Configure<TotpSettings>(Configuration.GetSection("Totp"));

            #endregion
            #region JwtTokenSetting
            JwtSettingModel jwtSetting = new JwtSettingModel();
            Configuration.Bind("JWT", jwtSetting);
            services.AddSingleton(jwtSetting);
            #endregion
            #region Swaager&Cors
            //Nuget.Project:Common.Api.Dependency
            services.AddOurSwagger();
            var corsOrigin = Configuration.GetSection("Cors:Origin").Get<string[]>();
            var corsMethod = Configuration.GetSection("Cors:Method").Get<string[]>();
            services.AddOurCors(corsOrigin, corsMethod);

            #endregion

            #endregion

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jwt.Identity.Api.Server v1"));
            }
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
