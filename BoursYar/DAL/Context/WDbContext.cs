using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

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
        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(dbModelBuilder);
            var conventions = new List<PluralizingTableNameConvention>().ToArray();
            dbModelBuilder.Conventions.Remove(conventions);
        }

        // public virtual  DbSet<CandelPattern> CandelPatterns { get; set; }

    }
}
