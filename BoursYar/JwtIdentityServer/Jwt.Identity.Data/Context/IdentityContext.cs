using System.Data.Common;
using Jwt.Identity.Data.IntialData;
using Jwt.Identity.Domain.Clients.Entity;
using Jwt.Identity.Domain.IdentityPolicy.Entity;
using Jwt.Identity.Domain.User.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Jwt.Identity.Data.Context
{

    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
            //اطمینان از ساخت دیتا بیس جدید
            //this.Database.EnsureCreated();
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<UserLogInOutLog> UserLogInOutLogs { get; set; }
        public DbSet<IdentitySettingPolicy> IdentitySettings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Client>()
                .HasIndex(b => b.ClientName)
                .IsUnique();
           // builder.Entity<IdentitySetting>().HasData(new IdentitySetting());

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}