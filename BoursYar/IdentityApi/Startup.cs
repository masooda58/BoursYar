using IdentityApi.Authoriz.ClaimBaseAuthoriz;
using IdentityApi.Config.Extention;
using IdentityApi.Config.Extention.Models;
using IdentityApi.Context;
using IdentityApi.Models;
using IdentityApi.Services.AuthClaimsGenrators;
using IdentityApi.Services.TokenGenrators;
using IdentityApi.Services.TokenValidators;
using IdentityApi.Services.UserManagementService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace IdentityApi
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
                    Configuration.GetConnectionString("IdetityDb")));


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            // bari gerftan jwt setting  az file appsetting.json estefadeh mishavad
            //var jwtSettingSection = Configuration.GetSection("JWT");
            //var jwtSetting = jwtSettingSection.Get<JwtSettingModel>();
            JwtSettingModel jwtSetting = new JwtSettingModel();
            Configuration.Bind("JWT", jwtSetting);

            //..........
            // services.Configure<JwtSettingModel>(JwtSettingSection);

            services.AddOurAuthentication(jwtSetting);

            //Authorization bari claim hai mokhtalef 
            services.AddAuthorization(options =>
            {
                options.AddPolicy("PolicyName", policy =>
                {
                    policy.RequireClaim("NameClaime", "value");
                });
            });

            #region corse

            var corsOrigin = Configuration.GetSection("Cors:Origin").Get<string[]>();
            var corsMethod = Configuration.GetSection("Cors:Method").Get<string[]>();
            services.AddOurCors(corsOrigin, corsMethod);

            #endregion

            services.AddHttpContextAccessor();
            services.AddClaimBaseAuthorize();
            services.AddControllers();
            services.AddOurSwagger();

            #region dependancy

            //dependances
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddSingleton<ITokenGenrators,TokenGenrator>();
            services.AddScoped<IAuthClaimsGenrators, AuthClaimsGenrators>();
            services.AddSingleton<TokenValidators>();
            services.AddSingleton(jwtSetting);
            //....
            #endregion
       
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiDataService v1"));
            }
            else
            {

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();


            app.UseCors();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //           name: "default",
            //            pattern: "{controller=Home}/{action=Index}/{id?}");


            //});
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name : "areas",
            //        pattern : "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            //    );
            //});

        }
    }
}
