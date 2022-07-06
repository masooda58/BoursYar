using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Interfaces.ITokenServices;
using Jwt.Identity.Domain.Interfaces.IUserRepositories;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Jwt.Identity.Api.Controllers
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
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        private readonly IAuthClaimsGenrators _claimsGenrators;

        private readonly ITokenGenrators _tokenGenrator;
        //private readonly UserManager<ApplicationUser> _userManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRefreshTokenRepository refreshTokenRepository, IAuthClaimsGenrators claimsGenrators, ITokenGenrators tokenGenrator)
        {
            _logger = logger;
            _refreshTokenRepository = refreshTokenRepository;
            _claimsGenrators = claimsGenrators;
            _tokenGenrator = tokenGenrator;

            //_userManager = userManager;
        }

        [HttpGet]
        [Route("test1")]
        public async Task<ActionResult>  tester()
        {
            //var user = new ApplicationUser()
            //{
            //    Email = "user@user.com",
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    UserName = "user"
            //};
            //var result = await _userManager.CreateAsync(user,"Ma@1234567890");
            return Ok();
        }

        [HttpGet]
        [Route("test")]
        public async Task<ActionResult> test()
        {
            var user = new ApplicationUser();
            var authClaims =await  _claimsGenrators.CreatClaims(user);

            var token = _tokenGenrator.GetAccessToken(authClaims);
           return Ok(token);
        }
    }
}
