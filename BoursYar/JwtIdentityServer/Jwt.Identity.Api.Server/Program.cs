﻿using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Jwt.Identity.Api.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var host = CreateHostBuilder(args).Build();

            //var scope = host.Services.CreateScope();

            //var ctx = scope.ServiceProvider.GetRequiredService<MyDbContext>(); 
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                
                #region Add Extra AppSetting.json

                // که در فولدر JwtSharedSetting های عمومی هستند درفایل Setting یکسری اطلاعات که
                // است و بصورت لینک به این پروژه آضافه شده قرار دارند SharedSetting 
                // اگر موردی در یکی از فایلها باشد آن مقدار فراخوان می شود
                // و اگر در دو فایل موجود باشد فایلی که در انتها
                // شده در خروجی لحاظ می شود AddJsonFile ترتیب خواندن به این شکل است که
                // در فولدر مربوطه باشد باید: Publish برای اینکه فایل لینک شده در پروژه در زمان
                // لحاظ گردد مانند زیر CopyToOutputDirectory="always" در فایل پروژه مقدار
                // <ItemGroup>
                //<Content Include="..\shared-Seting\TextFile1.json" Link="TextFile1.json"  CopyToOutputDirectory="always"/>
                //</ItemGroup>
                //start
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    //  config.Sources.Clear();
                    var env = hostingContext.HostingEnvironment;
                    // find the shared folder in the parent folder
                    //var sharedFolder = Path.Combine(env.ContentRootPath, "..", "Shared");
                    //load the SharedSettings first, so that appsettings.json overrwrites it
                    config
                        .AddJsonFile("appsettings.json", true)
                       .AddJsonFile("JwtIdentitySharedSettings.json", true)
                       .AddJsonFile($"SharedSettings.{env.EnvironmentName}.json", true);
                    config.AddEnvironmentVariables();
                }) //end

                #endregion
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                    logging.AddConsole();

                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}