using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace DAL
{
   public class WDbContext : DbContext
    {
   

        public virtual DbSet<AllNamadInfo> AllNamadInfo{ get; set; }
        public virtual DbSet<AllNamadInfo_Daily> AllNamadInfo_Daily { get; set; }
        public virtual DbSet<BourseIndex> BourseIndix { get; set; }
        public virtual DbSet<IndusteryIndex> IndusteryIndex { get; set; }
        public virtual DbSet<PayamNazer> PayamNazer{ get; set; }
        public virtual DbSet<AllNamadOption> AllNamadOption { get; set; }
        public virtual DbSet<Codal> Codal { get; set; }
        public virtual DbSet<FavNamad> FavNamad { get; set; }
        public virtual DbSet<IndNamad> IndNamad{ get; set; }
        public virtual DbSet<Crypto> Crypto { get; set; }
        public virtual DbSet<Arz> Arz{ get; set; }
        public virtual DbSet<Khodro> Khodro{ get; set; }
        public virtual DbSet<CallWebServiceSetting> CallWebServiceSetting { get; set; }
        public virtual DbSet<Logger> Logger { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(InitialSettingData.GetConnectionString("WDbContext"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var settings = InitialSettingData.Seed();

            modelBuilder.Entity<CallWebServiceSetting>().HasData(settings);
            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AS");
        }
       
        //protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        //{
        //    dbModelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //    base.OnModelCreating(dbModelBuilder);
        //    var conventions = new List<PluralizingTableNameConvention>().ToArray();
        //    dbModelBuilder.Conventions.Remove(conventions);
        //}

        // public virtual  DbSet<CandelPattern> CandelPatterns { get; set; }

    }
}
