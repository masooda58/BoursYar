using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common.Api.Dependency.Cors;
using Common.Api.Dependency.Swagger;
using Common.Jwt.Authentication;
using Jwt.Identity.Api.Server.Helpers.CustomSignIn;
using Jwt.Identity.Api.Server.Security;
using Jwt.Identity.Api.Server.Services.ConfirmCode;
using Jwt.Identity.Api.Server.Services.MessageServices;
using Jwt.Identity.Api.Server.Services.PhoneTotpProvider;
using Jwt.Identity.Api.Server.Services.TokenServices;
using Jwt.Identity.Data.Context;
using Jwt.Identity.Data.IntialData;
using Jwt.Identity.Data.Repositories.ClientRepository;
using Jwt.Identity.Data.Repositories.UserRepositories;
using Jwt.Identity.Data.UnitOfWork;
using Jwt.Identity.Domain.Clients.Data;
using Jwt.Identity.Domain.IServices;
using Jwt.Identity.Domain.IServices.Email;
using Jwt.Identity.Domain.IServices.Totp;
using Jwt.Identity.Domain.IServices.Totp.SettingModels;
using Jwt.Identity.Domain.Token.Data;
using Jwt.Identity.Domain.Token.ITokenServices;
using Jwt.Identity.Domain.User.Entities;
using Jwt.Identity.Domain.User.Enum;
using Jwt.Identity.Framework.Identity;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

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
                .AddUserManager<UserManagementService>()
                //email Token Provider
                .AddTokenProvider<EmailConfirmationTokenProvide<ApplicationUser>>("EmailConFirmation")
                .AddSignInManager<CustomSignInManager>() //custom signin manage for mobile or email
                .AddErrorDescriber<PersianIdentityErrorDescriber>();

            // تغییر زمان اعتبار همه توکن های ساخته شده
            services.Configure<DataProtectionTokenProviderOptions>(o => { o.TokenLifespan = TimeSpan.FromHours(8); });
            //تغییر زمان توکن ایمیل
            services.Configure<EmailConfirmationTokenProviderOptions>(o =>
            {
                o.TokenLifespan = TimeSpan.FromHours(2);
            });

            #endregion

            // ===== Configure Identity =======

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.Name = "auth_cookie";

            //    options.LoginPath = new PathString("/api/contests");
            //    options.AccessDeniedPath = new PathString("/api/contests");

            //    // Not creating a new object since ASP.NET Identity has created
            //    // one already and hooked to the OnValidatePrincipal event.
            //    // See https://github.com/aspnet/AspNetCore/blob/5a64688d8e192cacffda9440e8725c1ed41a30cf/src/Identity/src/Identity/IdentityServiceCollectionExtensions.cs#L56
            //    options.Events.OnRedirectToLogin = context =>
            //    {
            //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //        return Task.CompletedTask;
            //    };
            //});

            #region JwtTokenSetting

            var jwtSetting = new JwtSettingModel();
            Configuration.Bind("JWT", jwtSetting);
            services.AddSingleton(jwtSetting);

            #endregion

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "JWT_OR_COOKIE";
                    options.DefaultChallengeScheme = "JWT_OR_COOKIE";
                    //configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddCookie("Cookies", options =>
                {
                    options.LoginPath = "/Api/Acount/login";
                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                    options.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    };
                })
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = jwtSetting.ValidAudience,
                        ValidIssuer = jwtSetting.ValidIssuer,
                        RequireExpirationTime = true,
                        ClockSkew = TimeSpan.Zero,


                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["Access-TokenSession"];
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            var cache = context.HttpContext.RequestServices.GetService<IMemoryCache>();
                            var accessToken = context.SecurityToken as JwtSecurityToken;
                            var identity = context.Principal.Identity as ClaimsIdentity;
                            var tt = accessToken.RawData;
                            var UserID = identity.Claims.First(c => c.Type == "id").Value;
                            return Task.CompletedTask;
                        }
                    };
                })
                .AddPolicyScheme("JWT_OR_COOKIE", "JWT_OR_COOKIE", options =>
                {
                    // runs on each request
                    options.ForwardDefaultSelector = context =>
                    {
                        // filter by auth type
                        var reqHost = context.Request.Host.ToString();
                        var client = IntialClients.GetClients().SingleOrDefault(c => c.BaseUrl.Contains(reqHost));
                        if (client is { LoginType: LoginType.Token or LoginType.TokenAndCookie })
                        {
                            string authorization = context.Request.Headers[HeaderNames.Authorization];
                            if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                                return "Bearer";
                        }


                        if (client is { LoginType: LoginType.Cookie or LoginType.TokenAndCookie })
                        {
                            string accessToken = context.Request.Headers[HeaderNames.Cookie];
                            if (!string.IsNullOrEmpty(accessToken) && accessToken.Contains("Access-Token"))
                                return "Bearer";
                        }


                        // otherwise always check for cookie auth

                        return "Cookies";
                    };
                })
                .AddGoogle(option =>
                {
                    option.ClientId = "346095678950-dhuqj3ko64i5i1becqteg2v3rv9l8j6a.apps.googleusercontent.com";
                    option.ClientSecret = "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";
                });
            // 
            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    //JwtBearerDefaults.AuthenticationScheme,
                    //CookieAuthenticationDefaults.AuthenticationScheme,
                    "JWT_OR_COOKIE");
                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

            #region MemoryCache

            services.AddMemoryCache();

            #endregion

            #region Options from AppSetting

            #region Totp

            services.Configure<TotpSettings>(Configuration.GetSection("Totp"));

            #endregion

            #region Swaager&Cors

            //Nuget.Project:Common.Api.Dependency
            services.AddOurSwagger();
            var corsOrigin = Configuration.GetSection("Cors:Origin").Get<string[]>();
            var corsMethod = Configuration.GetSection("Cors:Method").Get<string[]>();
            services.AddOurCors(corsOrigin, corsMethod);

            #endregion

            #endregion

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    //   options.SuppressConsumesConstraintForFormFileParameters = true;
                    //   options.SuppressInferBindingSourcesForParameters = true;
                    // modelState مدیریت
                    options.SuppressModelStateInvalidFilter = true;
                    //   options.SuppressMapClientErrors = true;
                    //   options.ClientErrorMapping[StatusCodes.Status404NotFound].Link =
                    //      "/404";
                });
            services.AddHttpContextAccessor();
            services.AddMediatR(typeof(Startup).Assembly);
            #region dependancy

            services.AddScoped<UserManagementService>();
            services.AddTransient<UnitOfWork>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddSingleton<ITokenGenrators, TokenGenrators>();
            services.AddSingleton<ITokenValidators, TokenValidators>();
            services.AddSingleton<IAuthClaimsGenrators, AuthClaimsGenrators>();
           
            services.AddScoped<IEmailSender, EmailService>();
            services.AddScoped<ISmsSender, SmsServices>();
            services.AddTransient<IPhoneTotpProvider, PhoneTotpProvider>();
            services.AddScoped<ITotpCode, TotpCode>();
            services.AddScoped<IMailCode, MailCode>();
            services.AddSingleton<DataProtectionPepuseString>();

            #endregion

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CORSAllowLocalHost3000",
            //        builder =>
            //            builder.WithOrigins(new[] { "http://localhost:3000", "http://localhost:8080" })
            //                .AllowAnyHeader()
            //                .AllowAnyMethod()
            //                .AllowCredentials() // <<< this is required for cookies to be set on the client - sets the 'Access-Control-Allow-Credentials' to true
            //    );
            //});
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
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;
    
                await context.Response.WriteAsJsonAsync(new { error = exception.Message,Data=exception.Data });
            }));

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}