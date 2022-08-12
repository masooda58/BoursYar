using System;
using BoursYar.Authorization.IOC;
using Common.Api.Dependency.Cors;
using Common.Api.Dependency.Swagger;
using Common.Jwt.Authentication;
using Jwt.Identity.BoursYarServer.Services.TokenServices;
using Jwt.Identity.Data.Context;
using Jwt.Identity.Data.Repositories.UserRepositories;
using Jwt.Identity.Domain.Interfaces.ITokenServices;
using Jwt.Identity.Domain.Interfaces.IUserRepositories;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersianTranslation.Identity;

namespace Jwt.Identity.BoursYarServer
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
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("IdetityDb")), ServiceLifetime.Transient);


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
                    options.Lockout.MaxFailedAccessAttempts = 10;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<PersianIdentityErrorDescriber>();

            #region Authentication
            //Nuget.Project:Common.Jwt.Authentication
            JwtSettingModel jwtSetting = new JwtSettingModel();
            Configuration.Bind("JWT", jwtSetting);
            services.AddOurAuthentication(jwtSetting);
            #endregion
            services.AddControllersWithViews();
            services.AddRazorPages();

            #region Swaager&Cors
            //Nuget.Project:Common.Api.Dependency
            services.AddOurSwagger();
            var corsOrigin = Configuration.GetSection("Cors:Origin").Get<string[]>();
            var corsMethod = Configuration.GetSection("Cors:Method").Get<string[]>();
           services.AddOurCors(corsOrigin, corsMethod);

            #endregion

            #region Authorization

            //Nuget.Project:BoursYar.Authorization
            services.AddBoursYarAuthorize();

            #endregion
            services.AddHttpContextAccessor();

            #region dependancy
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddSingleton<ITokenGenrators, TokenGenrators>();
            services.AddSingleton<ITokenValidators, TokenValidators>();
            services.AddSingleton<IAuthClaimsGenrators, AuthClaimsGenrators>();
            services.AddTransient<IRoleManagementService, RoleManagementService>();
            services.AddTransient<IUserManagementService, UserManagementService>();
            #endregion

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jwt.Identity.BoursYarServer v1"));
            }
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
