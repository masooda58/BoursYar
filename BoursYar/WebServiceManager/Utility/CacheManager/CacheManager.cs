using DAL;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace WebServiceManager
{

    public class CacheManager
    {
        public static IMemoryCache Cache=new MemoryCache(new MemoryCacheOptions());

        public static EventHandler ChangeCache;

    

      //        public static void OnRemoveCache(string k, object v, CacheItemRemovedReason r)
        //    
        protected static void OnChangeCache(EventArgs e)
        {
            ChangeCache?.Invoke(null, e);
        }

        public static void EmptyCache_allnamadinfo()
        {
         
                RefreshCache_allnamadinfo();

                OnChangeCache(EventArgs.Empty);
           
          

        }

        public static void RefreshCache_allnamadinfo()
        {


            var qureylastdataallnamad = @"with ss as
        (
        select * from [WDb_Repository].[dbo].[AllNamadInfo] where ReqDateTime =
        (select max(ReqDateTime) from [WDb_Repository].[dbo].[AllNamadInfo])
        ),
        tt as
        (
        select *, max (ReqdateTime ) over (partition by name ) as maxdate
        from ss
        )
        select * from tt where ReqDateTime = maxdate ";

            using (var db = new UnitOfWorkDapper())
            {
                var d = db.AllnamadDapperRepository.GetQureyData(qureylastdataallnamad);
           Cache.Set("allnamadinfo", d, DateTimeOffset.MaxValue);

            }
          


        }

        public static void CacheData(string key, object value)
        {
          
            Cache.Set(key, value, DateTimeOffset.MaxValue);
            OnChangeCache(EventArgs.Empty);


        }
    }
}

