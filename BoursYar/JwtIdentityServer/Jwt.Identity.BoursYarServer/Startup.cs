﻿using Common.Api.Dependency.Cors;
using Common.Api.Dependency.Swagger;
using Jwt.Identity.BoursYarServer.Helpers.CustomSignIn;
using Jwt.Identity.BoursYarServer.OptionsModels;
using Jwt.Identity.BoursYarServer.Security;
using Jwt.Identity.BoursYarServer.Services.MessageServices;
using Jwt.Identity.BoursYarServer.Services.PhoneTotpProvider;
using Jwt.Identity.BoursYarServer.Services.TokenServices;
using Jwt.Identity.Data.Context;
using Jwt.Identity.Data.Repositories.UserRepositories;
using Jwt.Identity.Domain.Interfaces.IMessageSender;
using Jwt.Identity.Domain.Interfaces.IPhoneTotpProvider;
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
using System;

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
                     options.Lockout.MaxFailedAccessAttempts = 5;
                     options.Lockout.AllowedForNewUsers = true;

                     // User settings
                     options.User.RequireUniqueEmail = false;

                     // SignIn settings
                     //options.SignIn.RequireConfirmedEmail = false;
                     // options.SignIn.RequireConfirmedPhoneNumber = false;
                     options.SignIn.RequireConfirmedAccount = true;

                     //mobile token provider
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

            #region Authentication

            services.AddAuthentication()
                .AddCookie(options =>
                {
                    options.LoginPath = "Account/Login/";
                    options.AccessDeniedPath = "Account/AccessDenied/";
                    options.LogoutPath = "Account/Logout/";

                })
                .AddGoogle(option =>
                {
                    option.ClientId = "346095678950-dhuqj3ko64i5i1becqteg2v3rv9l8j6a.apps.googleusercontent.com";
                    option.ClientSecret = "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";
                });


            #endregion

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddHttpContextAccessor();

            #region dependancy
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddSingleton<ITokenGenrators, TokenGenrators>();
            services.AddSingleton<ITokenValidators, TokenValidators>();
            services.AddSingleton<IAuthClaimsGenrators, AuthClaimsGenrators>();
            services.AddTransient<IRoleManagementService, RoleManagementService>();
            services.AddTransient<IUserManagementService, UserManagementService>();
            services.AddScoped<IEmailSender, EmailService>();
            services.AddScoped<ISmsSender, SmsServices>();
            services.AddTransient<IPhoneTotpProvider, PhoneTotpProvider>();

            #endregion

            #region Options from AppSetting

            #region Totp

            services.Configure<TotpOptions>(Configuration.GetSection("Totp"));

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
            app.UseAuthentication();
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
