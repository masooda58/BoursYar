using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace WebApi
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

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<WDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("WDbContext")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
           

           
            services.AddControllersWithViews();
            services.AddScoped<UnitOfWorkDapper, UnitOfWorkDapper>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.SaveToken = true;
                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true, // اعتبار سنجی توکن سمت سرور
                        ValidateAudience = false, // سمت کلاینت اعتبار سنجی نشود
                        ValidateLifetime = true, //توکن مدت زمان دار است
                        ValidateIssuerSigningKey = true, // توکن اعتبار سنجی می شود
                        ValidAudience = Configuration["JWT:ValidAudience"],// در apsetting.json
                        ValidIssuer = Configuration["JWT:ValidIssuer"], //سروری که درخواستش معتبر است
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                    };
                });
            //services.AddCors(options =>
            //{
            //    services.AddCors(options =>
            //    {
            //        options.AddDefaultPolicy(builder => 
            //            builder.SetIsOriginAllowed(_ => true)
            //                .AllowAnyMethod()
            //                .AllowAnyHeader()
            //                .AllowCredentials());
            //    });
            //});
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("EnableCors", builder =>
            //    {
            //        builder.SetIsOriginAllowed(_ => true)
            //            .AllowAnyHeader()
            //            .AllowAnyMethod()
            //            .AllowCredentials()
            //            .Build();
            //    });


            //});
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => 
                    builder.SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
        
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseCors();
            app.UseRouting();
       
            app.UseAuthorization();
            app.UseAuthentication();
 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
 
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
 
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

        }
    }
}
