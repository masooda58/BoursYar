namespace DAL
{
    //public class UnitOfWork:IDisposable
    //{
        //WDbContext db = new WDbContext();
        //private IAllNamadInfoRepository<AllNamadInfo> _allNamadInfoRepository;
        //private IAllNamadInfoRepository<AllNamadInfo_Daily> _allNamaninfoInfo_DailyRepository;

        //public IAllNamadInfoRepository<AllNamadInfo> AllNamadInfoRepository
        //{
        //    get
        //    {
        //        if (_allNamadInfoRepository == null)
        //        {
        //            _allNamadInfoRepository= new AllNamadInfoRepository<AllNamadInfo>(db);
        //        }
        //        return _allNamadInfoRepository;
        //    }
        //}
        //public IAllNamadInfoRepository<AllNamadInfo_Daily> AllNamadInfo_DailyRepository
        //{
        //    get
        //    {
        //        if (_allNamaninfoInfo_DailyRepository== null)
        //        {
        //            _allNamaninfoInfo_DailyRepository = new AllNamadInfoRepository<AllNamadInfo_Daily>(db);
        //        }
        //        return _allNamaninfoInfo_DailyRepository;
        //    }
        //}
        //public virtual void save()
        //{
        //    db.SaveChanges();

        //}

        //public void Dispose()
        //{
        //   db.Dispose();
        //}
    //}
}
