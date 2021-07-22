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
        
        public IEnumerable BuyQueue()
        {
           // CacheManager.OnEmptyCache_allnamadinfo();
           // var data = (List<AllNamadInfo>) Program.Cache["allnamadinfo"];
            //&& l.DailyPriceHigh==l.HighestPrice
            return null;//data.Where(l => l.ClosePrice == l.DailyPriceHigh &&  l.TradeValue>0).ToList();
        }

        public IEnumerable SellQueue()
        {
           // CacheManager.OnEmptyCache_allnamadinfo();
            //var data = (List<AllNamadInfo>) Program.Cache["allNamadinfo"];
            //&& l.DailyPriceLow==l.HighestPrice
            return null;//data.Where(l => l.ClosePrice == l.DailyPriceLow &&  l.TradeValue>0).ToList();
        }

    }
}
