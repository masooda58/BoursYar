﻿using Jwt.Identity.BoursYarServer.Areas.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]
namespace Jwt.Identity.BoursYarServer.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddAntiforgery(options =>
                {
                    // Remote Validation جهت استفاده از
                    options.FormFieldName = "Input.__RequestVerificationToken";
                });
            });
        }
    }
}