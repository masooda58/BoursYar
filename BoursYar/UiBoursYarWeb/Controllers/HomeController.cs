using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ASP;
using DAL;
using Microsoft.Extensions.WebEncoders.Testing;
using UiBoursYarWeb.Models;
using WebServiceManager;


namespace UiBoursYarWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
         //ScheduleCallBack sb= new ScheduleCallBack("https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9&all&type=0");
         //sb.CallActionBack("allnamadinfo").Invoke();
         //CreatJob.RunAllTimer();
         //var x=StaticDictionary.NextRuns["allnamadinfo"];
        // testintialdaily();
            return View();
        }

        //public Task testintialdaily()
        //{
        //    return Task.Run(() =>
        //    {
        //        InitialDailyHistoryData.DailyNamdeInfo();
        //    });
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public IActionResult QueueBuy()
        //{
        //    return PartialView("_MiniTableTemplate");
        //}
    }
}
