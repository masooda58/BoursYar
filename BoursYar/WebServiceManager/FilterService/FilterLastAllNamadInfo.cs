using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Caching.Memory;


namespace WebServiceManager
{
    public class FilterLastAllNamadInfo : IFilterLastAllNamadInfo
    {
        
        public IEnumerable<AllNamadInfo> BuyQueue()
        {
            var value = CacheManager.Cache.Get("allnamadinfo");
            if (value==null)
            {
                CacheManager.EmptyCache_allnamadinfo();
                value=CacheManager.Cache.Get("allnamadinfo");
            }
             var data = (List<AllNamadInfo>) value;
            //&& l.DailyPriceHigh==l.HighestPrice
            return data.Where(l => l.ClosePrice == l.DailyPriceHigh &&  l.TradeValue>0).ToList();
        }
        public AllNamadInfo getnamdbyname(string code)
        {
            var value = CacheManager.Cache.Get("allnamadinfo");
            if (value==null)
            {
                CacheManager.EmptyCache_allnamadinfo();
                value=CacheManager.Cache.Get("allnamadinfo");
            }
            
            var data = (List<AllNamadInfo>) value;
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

        
    }
}
