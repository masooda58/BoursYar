using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jwt.Identity.BoursYarServer.Controllers
{
    [ApiController]
    [Route("AccountManage/[controller]")]
    public class AccountManagerController : Controller
    {
        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }
    }
}
