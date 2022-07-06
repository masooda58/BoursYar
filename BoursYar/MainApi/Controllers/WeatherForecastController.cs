using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BoursYar.Authorization.Attribute;
using BoursYar.Authorization.Utilities.MvcNameUtilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace MainApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMvcUtilities _utilities;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMvcUtilities utilities)
        {
            _logger = logger;
            _utilities = utilities;
        }
   
        [HttpGet]
        [Route("test")]
        [BoursYarAuthoriz(claimToAuthoriz:"x")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
      
        public ActionResult testauth()
        {
            var id = HttpContext.User.FindFirstValue("id");
            return Ok("is auth id="+id);
        }
        [HttpGet]
        [Route("getname")]
        public ActionResult getname()
        {

            var xx = _utilities.MvcInfo;
            return Ok(xx);
        }
        [HttpGet]
        [Route("GetTime")]
        public ActionResult getTime()
        {

            var xx = DateTime.Now;
            var ww = DateTime.Now.ToLocalTime();
            var gg = DateTime.Now.ToUniversalTime();
            var ff = DateTime.UtcNow.AddHours(4).AddMinutes(30);
            
            return Ok($"now={xx} local={ww} touniversal={gg} utcnow+1.30={ff} ");
        }
    }
}
