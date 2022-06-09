using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Areas.Identity.ContextDb;
using MainApi.Areas.Identity.Models;
using MainApi.Areas.Identity.Services.UserManagementService;
using MainApi.Config.Extention;
using MainApi.Config.Extention.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;


namespace MainApi
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
            var JwtSettingSection = Configuration.GetSection("JWT");
            var JwtSetting = JwtSettingSection.Get<JwtSettingModel>();
            //..........
            // services.Configure<JwtSettingModel>(JwtSettingSection);
            
            services.AddOurAuthentication(JwtSetting);

            //Authorization bari claim hai mokhtalef 
            services.AddAuthorization(options =>
            {
                options.AddPolicy("PolicyName", policy =>
                {
                    policy.RequireClaim("NameClaime", "value");
                });
            });
            // cors Settings
            var CorsOrigin= Configuration.GetSection("Cors:Origin").Get<string[]>();
            var CorsMethod= Configuration.GetSection("Cors:Method").Get<string[]>();
            services.AddOurCors(CorsOrigin,CorsMethod);
            //..
            services.AddControllers();
          services.AddOurSwagger();
          services.AddScoped<IUserManagementService, UserManagementService>();
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
