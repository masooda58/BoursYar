using Jwt.Identity.Domain.Models;
using Jwt.Identity.Domain.Models.Client;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jwt.Identity.Data.Context
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
       
        public DbSet<Client> Clients { get; set; }
        public DbSet<UserLogInOutLog> UserLogInOutLogs { get; set; }
        
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Client>()
                .HasIndex(b => b.ClientName)
                .IsUnique();

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}

