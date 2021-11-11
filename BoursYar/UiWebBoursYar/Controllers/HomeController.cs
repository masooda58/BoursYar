using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Services;
using UiWebBoursYar.Helpers;
using UiWebBoursYar.Models;
using WebServiceManager;

namespace UiWebBoursYar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UnitOfWorkDapper _db;

        public HomeController(ILogger<HomeController> logger, UnitOfWorkDapper db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
          
               CreatJob.RunAllTimer(); // run all timer
                //ViewBag.table =
                //    TableHelper.BuildTable(_db.CallwebservicesettingDapperRepository.GetAllData().ToList(),
                //        "table1");
                return View();

         

        }

        public IActionResult DatetimeResult ()
        {
            DateTime dt = DateTime.Now;
            return PartialView(dt);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public IActionResult detail(int id)
        {
           
                var setting = _db.CallwebservicesettingDapperRepository
                    .GetAllData().FirstOrDefault(i => i.Code == id);
                if (setting == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(setting);
                }
            


        }
        [HttpPost]
        public IActionResult detail(CallWebServiceSetting setting)

        {
            if (ModelState.IsValid)
            {
              
                    _db.CallwebservicesettingDapperRepository.UpdateData(setting);
              

                return Redirect("/Home/index");
            }

            return null;
        }


}
}
