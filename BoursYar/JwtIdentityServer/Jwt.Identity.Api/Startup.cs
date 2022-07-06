using BoursYar.Authorization.IOC;
using Common.Api.Dependency.Cors;
using Common.Api.Dependency.Swagger;
using Common.Jwt.Authentication;
using Jwt.Identity.Api.Services.TokenServices;
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

namespace Jwt.Identity.Api
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

            #region Authentication
            //Nuget.Project:Common.Jwt.Authentication
            JwtSettingModel jwtSetting = new JwtSettingModel();
            Configuration.Bind("JWT", jwtSetting);
            services.AddOurAuthentication(jwtSetting);
            #endregion


            services.AddControllers();

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
            

            #region dependancy
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddSingleton<ITokenGenrators, TokenGenrators>();
            services.AddSingleton<ITokenValidators, TokenValidators>();
            services.AddSingleton<IAuthClaimsGenrators, AuthClaimsGenrators>();
            #endregion

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jwt.Identity.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
