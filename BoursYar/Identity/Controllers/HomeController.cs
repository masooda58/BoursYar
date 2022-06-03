using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Model;
using Microsoft.AspNetCore.Identity;

namespace Identity.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> sakht()
        {

            var user = new ApplicationUser()
            {
                UserName = "masood",
                Email = "masoud@masoud.com",
                EmailConfirmed = true
            };
            var resualt =await _userManager.CreateAsync(user, "mA@1234567890");
            if (resualt.Succeeded)
            {
                return Ok("user masoud sakhteh shod");
            }
            return Ok("eradi pish amad");
        }
    }
}
