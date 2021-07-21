using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ClassLibrary2.Models
{
    public partial class WDb_RepositoryContext : DbContext
    {
        public WDb_RepositoryContext()
        {
        }

        public WDb_RepositoryContext(DbContextOptions<WDb_RepositoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AllNamadInfo> AllNamadInfos { get; set; }
        public virtual DbSet<AllNamadInfoDaily> AllNamadInfoDailies { get; set; }
        public virtual DbSet<AllNamadOption> AllNamadOptions { get; set; }
        public virtual DbSet<Arz> Arzs { get; set; }
        public virtual DbSet<ArzLastView> ArzLastViews { get; set; }
        public virtual DbSet<BourseIndex> BourseIndices { get; set; }
        public virtual DbSet<CallWebServiceSetting> CallWebServiceSettings { get; set; }
        public virtual DbSet<Codal> Codals { get; set; }
        public virtual DbSet<Crypto> Cryptos { get; set; }
        public virtual DbSet<FavNamad> FavNamads { get; set; }
        public virtual DbSet<IndNamad> IndNamads { get; set; }
        public virtual DbSet<IndusteryIndex> IndusteryIndices { get; set; }
        public virtual DbSet<Khodro> Khodros { get; set; }
        public virtual DbSet<Logger> Loggers { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }
        public virtual DbSet<NamadLastView> NamadLastViews { get; set; }
        public virtual DbSet<PayamNazer> PayamNazers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=WDb_Repository;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AS");

            modelBuilder.Entity<AllNamadInfo>(entity =>
            {
                entity.ToTable("AllNamadInfo");

                entity.Property(e => e.MiladiDate).HasColumnType("datetime");

                entity.Property(e => e.Pe).HasColumnName("PE");

                entity.Property(e => e.ReqDateTime).HasColumnType("datetime");

                entity.Property(e => e.The1BuyCount).HasColumnName("The1_BuyCount");

                entity.Property(e => e.The1BuyPrice).HasColumnName("The1_BuyPrice");

                entity.Property(e => e.The1BuyVolume).HasColumnName("The1_BuyVolume");

                entity.Property(e => e.The1SellCount).HasColumnName("The1_SellCount");

                entity.Property(e => e.The1SellPrice).HasColumnName("The1_SellPrice");

                entity.Property(e => e.The1SellVolume).HasColumnName("The1_SellVolume");

                entity.Property(e => e.The2BuyCount).HasColumnName("The2_BuyCount");

                entity.Property(e => e.The2BuyPrice).HasColumnName("The2_BuyPrice");

                entity.Property(e => e.The2BuyVolume).HasColumnName("The2_BuyVolume");

                entity.Property(e => e.The2SellCount).HasColumnName("The2_SellCount");

                entity.Property(e => e.The2SellPrice).HasColumnName("The2_SellPrice");

                entity.Property(e => e.The2SellVolume).HasColumnName("The2_SellVolume");

                entity.Property(e => e.The3BuyCount).HasColumnName("The3_BuyCount");

                entity.Property(e => e.The3BuyPrice).HasColumnName("The3_BuyPrice");

                entity.Property(e => e.The3BuyVolume).HasColumnName("The3_BuyVolume");

                entity.Property(e => e.The3SellCount).HasColumnName("The3_SellCount");

                entity.Property(e => e.The3SellPrice).HasColumnName("The3_SellPrice");

                entity.Property(e => e.The3SellVolume).HasColumnName("The3_SellVolume");
            });

            modelBuilder.Entity<AllNamadInfoDaily>(entity =>
            {
                entity.ToTable("AllNamadInfo_Daily");

                entity.Property(e => e.MiladiDate).HasColumnType("datetime");

                entity.Property(e => e.Pe).HasColumnName("PE");

                entity.Property(e => e.ReqDateTime).HasColumnType("datetime");

                entity.Property(e => e.The1BuyCount).HasColumnName("The1_BuyCount");

                entity.Property(e => e.The1BuyPrice).HasColumnName("The1_BuyPrice");

                entity.Property(e => e.The1BuyVolume).HasColumnName("The1_BuyVolume");

                entity.Property(e => e.The1SellCount).HasColumnName("The1_SellCount");

                entity.Property(e => e.The1SellPrice).HasColumnName("The1_SellPrice");

                entity.Property(e => e.The1SellVolume).HasColumnName("The1_SellVolume");

                entity.Property(e => e.The2BuyCount).HasColumnName("The2_BuyCount");

                entity.Property(e => e.The2BuyPrice).HasColumnName("The2_BuyPrice");

                entity.Property(e => e.The2BuyVolume).HasColumnName("The2_BuyVolume");

                entity.Property(e => e.The2SellCount).HasColumnName("The2_SellCount");

                entity.Property(e => e.The2SellPrice).HasColumnName("The2_SellPrice");

                entity.Property(e => e.The2SellVolume).HasColumnName("The2_SellVolume");

                entity.Property(e => e.The3BuyCount).HasColumnName("The3_BuyCount");

                entity.Property(e => e.The3BuyPrice).HasColumnName("The3_BuyPrice");

                entity.Property(e => e.The3BuyVolume).HasColumnName("The3_BuyVolume");

                entity.Property(e => e.The3SellCount).HasColumnName("The3_SellCount");

                entity.Property(e => e.The3SellPrice).HasColumnName("The3_SellPrice");

                entity.Property(e => e.The3SellVolume).HasColumnName("The3_SellVolume");
            });

            modelBuilder.Entity<AllNamadOption>(entity =>
            {
                entity.ToTable("AllNamadOption");

                entity.Property(e => e.MiladiDate).HasColumnType("datetime");

                entity.Property(e => e.Pe).HasColumnName("PE");

                entity.Property(e => e.ReqDateTime).HasColumnType("datetime");

                entity.Property(e => e.The1BuyCount).HasColumnName("The1_BuyCount");

                entity.Property(e => e.The1BuyPrice).HasColumnName("The1_BuyPrice");

                entity.Property(e => e.The1BuyVolume).HasColumnName("The1_BuyVolume");

                entity.Property(e => e.The1SellCount).HasColumnName("The1_SellCount");

                entity.Property(e => e.The1SellPrice).HasColumnName("The1_SellPrice");

                entity.Property(e => e.The1SellVolume).HasColumnName("The1_SellVolume");

                entity.Property(e => e.The2BuyCount).HasColumnName("The2_BuyCount");

                entity.Property(e => e.The2BuyPrice).HasColumnName("The2_BuyPrice");

                entity.Property(e => e.The2BuyVolume).HasColumnName("The2_BuyVolume");

                entity.Property(e => e.The2SellCount).HasColumnName("The2_SellCount");

                entity.Property(e => e.The2SellPrice).HasColumnName("The2_SellPrice");

                entity.Property(e => e.The2SellVolume).HasColumnName("The2_SellVolume");

                entity.Property(e => e.The3BuyCount).HasColumnName("The3_BuyCount");

                entity.Property(e => e.The3BuyPrice).HasColumnName("The3_BuyPrice");

                entity.Property(e => e.The3BuyVolume).HasColumnName("The3_BuyVolume");

                entity.Property(e => e.The3SellCount).HasColumnName("The3_SellCount");

                entity.Property(e => e.The3SellPrice).HasColumnName("The3_SellPrice");

                entity.Property(e => e.The3SellVolume).HasColumnName("The3_SellVolume");
            });

            modelBuilder.Entity<Arz>(entity =>
            {
                entity.ToTable("Arz");

                entity.Property(e => e.MiladiDate).HasColumnType("datetime");

                entity.Property(e => e.ReqDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ArzLastView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Arz_Last_View");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Maxdate)
                    .HasColumnType("datetime")
                    .HasColumnName("maxdate");

                entity.Property(e => e.MiladiDate).HasColumnType("datetime");

                entity.Property(e => e.ReqDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<BourseIndex>(entity =>
            {
                entity.ToTable("BourseIndex");

                entity.Property(e => e.IndexHchange).HasColumnName("IndexHChange");

                entity.Property(e => e.IndexHchangePercent).HasColumnName("IndexHChangePercent");

                entity.Property(e => e.Market).IsRequired();

                entity.Property(e => e.MiladiDate).HasColumnType("datetime");

                entity.Property(e => e.ReqDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<CallWebServiceSetting>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK_dbo.CallWebServiceSetting");

                entity.ToTable("CallWebServiceSetting");

                entity.Property(e => e.Code).ValueGeneratedNever();
            });

            modelBuilder.Entity<Codal>(entity =>
            {
                entity.ToTable("Codal");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Excel).HasColumnName("excel");

                entity.Property(e => e.LetterNumber).HasColumnName("letter_number");

                entity.Property(e => e.Link).HasColumnName("link");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Pdf).HasColumnName("pdf");

                entity.Property(e => e.Symbol).HasColumnName("symbol");

                entity.Property(e => e.Title).HasColumnName("title");
            });

            modelBuilder.Entity<Crypto>(entity =>
            {
                entity.ToTable("Crypto");

                entity.Property(e => e.Date).HasColumnType("datetime");
            });

            modelBuilder.Entity<FavNamad>(entity =>
            {
                entity.ToTable("FavNamad");

                entity.Property(e => e.Market).IsRequired();

                entity.Property(e => e.MiladiDate).HasColumnType("datetime");

                entity.Property(e => e.ReqDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<IndNamad>(entity =>
            {
                entity.ToTable("IndNamad");

                entity.Property(e => e.Market).IsRequired();

                entity.Property(e => e.MiladiDate).HasColumnType("datetime");

                entity.Property(e => e.ReqDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<IndusteryIndex>(entity =>
            {
                entity.ToTable("IndusteryIndex");

                entity.Property(e => e.MiladiDate).HasColumnType("datetime");

                entity.Property(e => e.ReqDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Khodro>(entity =>
            {
                entity.ToTable("Khodro");
            });

            modelBuilder.Entity<Logger>(entity =>
            {
                entity.ToTable("Logger");

                entity.Property(e => e.ReqTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<NamadLastView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Namad_Last_View");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Maxdate)
                    .HasColumnType("datetime")
                    .HasColumnName("maxdate");

                entity.Property(e => e.MiladiDate).HasColumnType("datetime");

                entity.Property(e => e.Pe).HasColumnName("PE");

                entity.Property(e => e.ReqDateTime).HasColumnType("datetime");

                entity.Property(e => e.The1BuyCount).HasColumnName("The1_BuyCount");

                entity.Property(e => e.The1BuyPrice).HasColumnName("The1_BuyPrice");

                entity.Property(e => e.The1BuyVolume).HasColumnName("The1_BuyVolume");

                entity.Property(e => e.The1SellCount).HasColumnName("The1_SellCount");

                entity.Property(e => e.The1SellPrice).HasColumnName("The1_SellPrice");

                entity.Property(e => e.The1SellVolume).HasColumnName("The1_SellVolume");

                entity.Property(e => e.The2BuyCount).HasColumnName("The2_BuyCount");

                entity.Property(e => e.The2BuyPrice).HasColumnName("The2_BuyPrice");

                entity.Property(e => e.The2BuyVolume).HasColumnName("The2_BuyVolume");

                entity.Property(e => e.The2SellCount).HasColumnName("The2_SellCount");

                entity.Property(e => e.The2SellPrice).HasColumnName("The2_SellPrice");

                entity.Property(e => e.The2SellVolume).HasColumnName("The2_SellVolume");

                entity.Property(e => e.The3BuyCount).HasColumnName("The3_BuyCount");

                entity.Property(e => e.The3BuyPrice).HasColumnName("The3_BuyPrice");

                entity.Property(e => e.The3BuyVolume).HasColumnName("The3_BuyVolume");

                entity.Property(e => e.The3SellCount).HasColumnName("The3_SellCount");

                entity.Property(e => e.The3SellPrice).HasColumnName("The3_SellPrice");

                entity.Property(e => e.The3SellVolume).HasColumnName("The3_SellVolume");
            });

            modelBuilder.Entity<PayamNazer>(entity =>
            {
                entity.ToTable("PayamNazer");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
