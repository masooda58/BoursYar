using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Admin.UI.MVC.Models
{
    public partial class BoursYarIdentityDbContext : DbContext
    {
        public BoursYarIdentityDbContext()
        {
        }

        public BoursYarIdentityDbContext(DbContextOptions<BoursYarIdentityDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationUserPolicy> ApplicationUserPolicies { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<IdentitySetting> IdentitySettings { get; set; }
        public virtual DbSet<SessionEntity> SessionEntities { get; set; }
        public virtual DbSet<UserLogInOutLog> UserLogInOutLogs { get; set; }
        public virtual DbSet<UserLoginPolicyOption> UserLoginPolicyOptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=BoursYarIdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AS");

            modelBuilder.Entity<ApplicationUserPolicy>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PolicyId });

                entity.HasIndex(e => e.PolicyId, "AK_ApplicationUserPolicies_PolicyId")
                    .IsUnique();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ApplicationUserPolicies)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasIndex(e => e.ClientName, "IX_Clients_ClientName")
                    .IsUnique()
                    .HasFilter("([ClientName] IS NOT NULL)");
            });

            modelBuilder.Entity<SessionEntity>(entity =>
            {
                entity.HasKey(e => e.SessionId);

                entity.ToTable("SessionEntity");
            });

            modelBuilder.Entity<UserLogInOutLog>(entity =>
            {
                entity.HasKey(e => e.IdGuid);

                entity.HasIndex(e => e.SessionId, "IX_UserLogInOutLogs_SessionId");

                entity.Property(e => e.IdGuid).ValueGeneratedNever();

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.UserLogInOutLogs)
                    .HasForeignKey(d => d.SessionId);
            });

            modelBuilder.Entity<UserLoginPolicyOption>(entity =>
            {
                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.UserLoginPolicyOption)
                    .HasPrincipalKey<ApplicationUserPolicy>(p => p.PolicyId)
                    .HasForeignKey<UserLoginPolicyOption>(d => d.Id);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
