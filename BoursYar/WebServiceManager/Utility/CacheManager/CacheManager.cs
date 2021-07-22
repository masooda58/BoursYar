using DAL;
using System;

namespace WebServiceManager
{

//    public class CacheManager
//    {

//        private static CacheItemRemovedCallback onRemove = null;
//        public static EventHandler ChangeCache;
//        public static void OnRemoveCache(string k, object v, CacheItemRemovedReason r)
//        {

//        }
//        protected static void OnChangeCache(EventArgs e)
//        {
//            ChangeCache?.Invoke(null, e);
//        }

//        public static void OnEmptyCache_allnamadinfo()
//        {

          
//                if (Program.Cache.Get("allnamadinfo") == null)
//                {
//                    OnRefreshCache_allnamadinfo();
//                }
           
         
//        }

//        public static void OnRefreshCache_allnamadinfo()
//        {
          
               
//                var qureylastdataallnamad = @"with ss as
//(
//select * from [WDb_Repository].[dbo].[AllNamadInfo] where ReqDateTime =
//(select max(ReqDateTime) from [WDb_Repository].[dbo].[AllNamadInfo])
//),
//tt as
//(
//select *, max (ReqdateTime ) over (partition by name ) as maxdate
//from ss
//)
//select * from tt where ReqDateTime = maxdate ";

//                using (var db = new UnitOfWorkDapper())
//                {
//                    var d = db.AllnamadDapperRepository.GetQureyData(qureylastdataallnamad);
//                    Program.Cache.Add("allnamadinfo",d,DateTimeOffset.MaxValue);
                 
//                }
//                OnChangeCache(EventArgs.Empty);
          

//        }

//        public static void CacheData(string key, object value)
//        {
//            Program.Cache.Remove(key);
//            Program.Cache.Add(key, value,DateTimeOffset.MaxValue);
//            OnChangeCache(EventArgs.Empty);


//        }
//    }
}

