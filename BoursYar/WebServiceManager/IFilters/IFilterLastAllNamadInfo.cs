using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceManager
{
    public interface IFilterLastAllNamadInfo
    {
        IEnumerable<AllNamadInfo> BuyQueue();
        IEnumerable SellQueue();



    }
}
