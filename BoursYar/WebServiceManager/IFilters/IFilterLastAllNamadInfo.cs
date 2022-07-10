using DAL;
using System.Collections;
using System.Collections.Generic;

namespace WebServiceManager
{
    public interface IFilterLastAllNamadInfo
    {
        IEnumerable<AllNamadInfo> BuyQueue();
        IEnumerable SellQueue();



    }
}
