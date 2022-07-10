using Jwt.Identity.Domain.Interfaces.ITokenServices;
using Jwt.Identity.Domain.Interfaces.IUserRepositories;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        private readonly IRoleManagementService _roleManagement;
        //private readonly UserManager<ApplicationUser> _userManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRefreshTokenRepository refreshTokenRepository, IAuthClaimsGenrators claimsGenrators, ITokenGenrators tokenGenrator, IRoleManagementService roleManagement)
        {
            _logger = logger;
            _refreshTokenRepository = refreshTokenRepository;
            _claimsGenrators = claimsGenrators;
            _tokenGenrator = tokenGenrator;
            _roleManagement = roleManagement;

            //_userManager = userManager;
        }

        [HttpGet]
        [Route("test1")]
        public async Task<ActionResult> tester()
        {
            List<string> rolesNames = new List<string>();
            rolesNames.Add("Admin");
            rolesNames.Add("Regular");
            var c = await _roleManagement.CreateRoleAsync("Admin");
            var role = await _roleManagement.FindRoleByNameAsync("Admin");
            var t = await _roleManagement.DeleteRoleAsync(role);
            var cc = await _roleManagement.CreateRoleAsync("Admin");
            await _roleManagement.DeleteRolesByNameAsync(rolesNames);


            return Ok();

        }

        [HttpGet]
        [Route("test")]
        public async Task<ActionResult> test()
        {
            var user = new ApplicationUser();
            var authClaims = await _claimsGenrators.CreatClaims(user);

            var token = _tokenGenrator.GetAccessToken(authClaims);
            return Ok(token);
        }
    }
}
