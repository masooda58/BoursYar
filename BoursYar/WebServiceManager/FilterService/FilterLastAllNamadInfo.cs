using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebServiceManager
{
    public class FilterLastAllNamadInfo : IFilterLastAllNamadInfo
    {
        
        public IEnumerable<AllNamadInfo> BuyQueue()
        {
            // CacheManager.OnEmptyCache_allnamadinfo();
            var data = (List<AllNamadInfo>) getlastdata();
            //&& l.DailyPriceHigh==l.HighestPrice
            return data.Where(l => l.ClosePrice == l.DailyPriceHigh &&  l.TradeValue>0).ToList();
        }
        public AllNamadInfo getnamdbyname(string code)
        {
            // CacheManager.OnEmptyCache_allnamadinfo();
            
            var data = (List<AllNamadInfo>) getlastdata();;
            if (data != null)
            {
                return data.Where(l=>l.NamadCode==code).FirstOrDefault();
            }
            //&& l.DailyPriceHigh==l.HighestPrice
           
            else
            {
                return null;
            }
        }


        public IEnumerable SellQueue()
        {
           // CacheManager.OnEmptyCache_allnamadinfo();
            //var data = (List<AllNamadInfo>) Program.Cache["allNamadinfo"];
            //&& l.DailyPriceLow==l.HighestPrice
            return null;//data.Where(l => l.ClosePrice == l.DailyPriceLow &&  l.TradeValue>0).ToList();
        }

        public object getlastdata()
        {
            var qureylastdataallnamad = @"with ss as
(
select * from [WDb_1].[dbo].[AllNamadInfo] where ReqDateTime =
(select max(ReqDateTime) from [WDb_1].[dbo].[AllNamadInfo])
),
tt as
(
select *, max (ReqdateTime ) over (partition by name ) as maxdate
from ss
)
select * from tt where ReqDateTime = maxdate ";

            using (var db = new UnitOfWorkDapper())
            {
                var d = db.AllnamadDapperRepository.GetQureyData(qureylastdataallnamad).ToList();
                return d;
            }
        
        }
    }
}
