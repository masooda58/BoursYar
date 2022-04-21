using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class testController : ControllerBase
    {
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
       // [Authorize]
        public ActionResult Get()
        {
            return Ok("salam");
        }
    }
}
