using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebServiceManager;

namespace UiBoursYarWeb.Component
{
    public class BQ:ViewComponent
    {
        public async Task<IViewComponentResult>  InvokeAsync()
        {
           
                FilterLastAllNamadInfo filter = new FilterLastAllNamadInfo();
                var d = filter.BuyQueue();
                return View("/Views/Home/_MiniTableTemplate.cshtml",d);
            
        }
    }
}
