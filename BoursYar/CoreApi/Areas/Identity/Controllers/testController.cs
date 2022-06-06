using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreApi.Areas.Identity.Controllers
{
    [Route("Identity/[controller]/[Action]")]
    [ApiController]
    public class testController : ControllerBase
    {
        // GET: api/<testController>
        [HttpGet]
        public IEnumerable<string> masoud()
        {
            return new string[] { "value1", "value2" };
        }

    }
}
