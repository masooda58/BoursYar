using IdentityApi.Models;
using IdentityApi.Models.Requests;
using IdentityApi.Models.Response;
using IdentityApi.Services.AuthClaimsGenrators;
using IdentityApi.Services.TokenGenrators;
using IdentityApi.Services.TokenValidators;
using IdentityApi.Services.UserManagementService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using RefreshTokenRequest = IdentityApi.Models.Requests.RefreshTokenRequest;

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
        private readonly ITokenGenrators _tokenGenrator;
        private readonly IAuthClaimsGenrators _claimsGenrators;
        private readonly TokenValidators _tokenValidators;
        private readonly SignInManager<ApplicationUser> _uSignInManager;
        public HomeController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration,
            IUserManagementService userManagementService, ITokenGenrators tokenGenrator,
            IAuthClaimsGenrators claimsGenrators, TokenValidators tokenValidators, SignInManager<ApplicationUser> uSignInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _userManagementService = userManagementService;
            _tokenGenrator = tokenGenrator;
            _claimsGenrators = claimsGenrators;
            _tokenValidators = tokenValidators;
            _uSignInManager = uSignInManager;
        }

        [HttpGet]
        [Route("index")]
        public IActionResult Index()
        {
            return Ok("salam");
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

                var authClaims = await _claimsGenrators.CreatClaims(user);

                var token = _tokenGenrator.GetAccessToken(authClaims);
                await _userManagementService.DeleteRefreshTokenByuserId(user.Id);
                await _userManagementService.WritRefreshTokenAsync(user.Id, token.RefreshToken);
                Response.Cookies.Append("jwt", token.AccessToken, new CookieOptions() { HttpOnly = true });
                var cc = await _uSignInManager.PasswordSignInAsync(model.Username, model.Password, true, true);
                return Ok(token);
            }
            return Unauthorized();
        }

        [HttpPost("Refresh")]
        public async Task<ActionResult> Refresh([FromBody] RefreshTokenRequest refreshToken)
        {


            bool isValidRefreshToken = _tokenValidators.Validate(refreshToken.Refreshtoken);
            if (!isValidRefreshToken)
            {
                return BadRequest("توکن معتبر نیست");
            }

            string userId = await _userManagementService.GetUserIdByRefreshToken(refreshToken.Refreshtoken);
            if (userId == null) return Unauthorized();
            var user = await _userManager.FindByIdAsync(userId);
            var authClaims = await _claimsGenrators.CreatClaims(user);

            var token = _tokenGenrator.GetAccessToken(authClaims);
            await _userManagementService.DeleteRefreshTokenByuserId(userId);
            await _userManagementService.WritRefreshTokenAsync(user.Id, token.RefreshToken);

            return Ok(token);

        }

        [Authorize]
        [HttpGet("test")]
        public async Task<ActionResult> test()
        {
            var id = HttpContext.User.FindFirstValue("id");
            var x = await _userManagementService.DeleteRefreshTokenByuserId(id);
            var g = HttpContext.Request.Cookies;
            return Ok(g);
        }
        [HttpGet]
        [Route("addClaimToRole")]
        [Authorize]
        public async Task addClaimsToRole()
        {
            // var role = new IdentityRole("admin");
            // var v=  await _roleManager.CreateAsync(role);
            // var x= await _roleManager.AddClaimAsync(role, new Claim("per", "manage"));
            var userid = HttpContext.User.FindFirstValue("id");
            var user = await _userManager.FindByIdAsync(userid);
            await _userManager.AddToRoleAsync(user, "admin");
        }
    }
}

