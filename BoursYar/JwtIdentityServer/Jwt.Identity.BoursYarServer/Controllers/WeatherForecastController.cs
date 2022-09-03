using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Interfaces.ITokenServices;
using Jwt.Identity.Domain.Interfaces.IUserRepositories;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Jwt.Identity.BoursYarServer.Controllers
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRefreshTokenRepository refreshTokenRepository, IAuthClaimsGenrators claimsGenrators, ITokenGenrators tokenGenrator, IRoleManagementService roleManagement,  UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _refreshTokenRepository = refreshTokenRepository;
            _claimsGenrators = claimsGenrators;
            _tokenGenrator = tokenGenrator;
            _roleManagement = roleManagement;
            _userManager = userManager;
            _signInManager = signInManager;

            //_userManager = userManager;
        }
        [HttpPost]
       [ValidateAntiForgeryToken]
        public JsonResult CheckEmail([FromForm][Bind( Prefix = "Input.Email")] string email)
        {
            
            var user = _userManager.FindByEmailAsync(email).Result;
            var valid = user == null;
            return new JsonResult(valid);

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
        [Authorize]
        public async Task<ActionResult> test()
        {
            var user = new ApplicationUser();
            var authClaims = await _claimsGenrators.CreatClaims(user);

            var token = _tokenGenrator.GetAccessToken(authClaims);
            return Ok(token);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.username);
            var x = await _userManager.CheckPasswordAsync(user, model.password);
            var d=await _signInManager.PasswordSignInAsync(model.username, model.password, false,false);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.password))
            {

                var authClaims = await _claimsGenrators.CreatClaims(user);

                var token = _tokenGenrator.GetAccessToken(authClaims);
                await _refreshTokenRepository.DeleteRefreshTokenByuserIdAsync(user.Id);
                await _refreshTokenRepository.WritRefreshTokenAsync(user.Id, token.RefreshToken);
              Response.Cookies.Append("jwt", token.AccessToken, new CookieOptions() { HttpOnly = true,Secure = true });
               // var cc = await _uSignInManager.PasswordSignInAsync(model.Username, model.Password, true, true);
                return Ok(token);
            }
            return Unauthorized();
        }
        //[HttpPost]
        //[Route("register")]

       // public async Task<IActionResult> Register([FromBody] RegisterModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var errorList = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
        //        return BadRequest(new{errorList});
        //    }
        //    {
            
              
        //        var user = new ApplicationUser()
        //        {
        //            Email = model.Email,
        //            SecurityStamp = Guid.NewGuid().ToString(),
        //            UserName = model.Username
        //        };
        //        var result = await _userManager.CreateAsync(user, model.Password);
        //        if (!result.Succeeded)
        //            return Ok("error");

        //        return Ok("user creat");
        //    }
        //}
    }
    

    public class LoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }
  
        //public class RegisterModel
        //{
        //    [Required(ErrorMessage = "نام کاربری را وراد کنید")]
        //    public string? Username { get; set; }

        //    [EmailAddress(ErrorMessage = "ایمیل معتبر نیست")]
        //    [Required(ErrorMessage = "ایمیل را وارد کنید")]
        //    public string? Email { get; set; }

        //    [Required(ErrorMessage = "Password is required")]
        //    public string? Password { get; set; }
        //}
 
}
