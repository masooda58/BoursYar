using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityApi.Models;
using IdentityApi.Models.Requests;
using IdentityApi.Models.Response;
using IdentityApi.Services.TokenGenrators;
using IdentityApi.Services.UserManagementService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IdentityApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUserManagementService _userManagementService;
        private readonly TokenGenrator _tokenGenrator;

        public HomeController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IUserManagementService userManagementService, TokenGenrator tokenGenrator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _userManagementService = userManagementService;
            _tokenGenrator = tokenGenrator;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("sala");
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new ApiErrorResponse(errorList));
            }
            {
                var userExists = await _userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("name","masoud"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = _tokenGenrator.GetToken(authClaims);

                return Ok(token);
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("all")]
        [Authorize]
        public IActionResult getall()
        {
           return Ok(" نمایش فقط برای ادمین");
        }
        [HttpPost]
        [Route("changerole")]
       
        public async Task<IActionResult> chang([FromBody] RegisterModel model)
        {
          
            var user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
          var r=  await _userManagementService.AddUserAsync(user, model.Password, "user");
            return Ok(r);
        }

    }
}

