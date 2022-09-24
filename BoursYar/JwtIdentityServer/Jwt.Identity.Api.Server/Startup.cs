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
using Jwt.Identity.Api.Server.Services.ConfirmCode;
using Jwt.Identity.Api.Server.Services.MessageServices;
using Jwt.Identity.Api.Server.Services.PhoneTotpProvider;
using Jwt.Identity.Api.Server.Services.TokenServices;
using Jwt.Identity.Data.Repositories;
using Jwt.Identity.Data.Repositories.UserRepositories;
using Jwt.Identity.Domain.Interfaces.IConfirmCode;
using Jwt.Identity.Domain.Interfaces.IMessageSender;
using Jwt.Identity.Domain.Interfaces.IPhoneTotpProvider;
using Jwt.Identity.Domain.Interfaces.IRepository;
using Jwt.Identity.Domain.Interfaces.ITokenServices;
using Jwt.Identity.Domain.Interfaces.IUserRepositories;
using Jwt.Identity.Domain.Models.Client;
using Jwt.Identity.Domain.Models.SettingModels;
using Microsoft.AspNetCore.Http;

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

            services.AddAuthentication()
                .AddGoogle(option =>
            {
                option.ClientId = "346095678950-dhuqj3ko64i5i1becqteg2v3rv9l8j6a.apps.googleusercontent.com";
                option.ClientSecret = "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";
            });

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
          //  services.AddOurCors(corsOrigin, corsMethod);

            #endregion

            #endregion

           services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                 //   options.SuppressConsumesConstraintForFormFileParameters = true;
                 //   options.SuppressInferBindingSourcesForParameters = true;
                    options.SuppressModelStateInvalidFilter = true;
                 //   options.SuppressMapClientErrors = true;
                 //   options.ClientErrorMapping[StatusCodes.Status404NotFound].Link =
                  //      "/404";
                });
            services.AddHttpContextAccessor();

            #region dependancy
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddSingleton<ITokenGenrators, TokenGenrators>();
            services.AddSingleton<ITokenValidators, TokenValidators>();
            services.AddSingleton<IAuthClaimsGenrators, AuthClaimsGenrators>();
            services.AddScoped<IRepositoryService<Client>, RepositoryService<Client>>();
            services.AddScoped<IEmailSender, EmailService>();
            services.AddScoped<ISmsSender, SmsServices>();
            services.AddTransient<IPhoneTotpProvider, PhoneTotpProvider>();
            services.AddScoped<ITotpCode, TotpCode>();
            services.AddScoped<IMailCode, MailCode>();
            services.AddSingleton<DataProtectionPepuseString>();

            #endregion
            services.AddCors(options =>
            {
                options.AddPolicy("CORSAllowLocalHost3000",
                    builder =>
                        builder.WithOrigins(new[] { "http://localhost:3000", "http://localhost:8080" })
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials() // <<< this is required for cookies to be set on the client - sets the 'Access-Control-Allow-Credentials' to true
                );
            });
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
            app.UseCors(builder =>
            {
                builder
                    .WithOrigins("http://localhost:3000", "https://example.com")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
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
