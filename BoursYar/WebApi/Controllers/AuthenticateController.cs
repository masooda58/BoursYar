using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthenticateController(UserManager<IdentityUser> userManager,
            IConfiguration configuration,SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                //var userExists = await _userManager.FindByNameAsync(model.Username);

                //if (userExists != null)

                //{

                //    return StatusCode(StatusCodes.Status500InternalServerError,
                //        new Response { Status = "Error", Message = "User already exists!" });
                //}
                //var emailExists = await _userManager.FindByEmailAsync(model.Email);
                //if (emailExists != null)
                //{

                //    return StatusCode(StatusCodes.Status500InternalServerError,
                //        new Response {Status = "Error", Message = "Email already exists!"});
                //}
                var user = new IdentityUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username.ToLower()
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    var e = "";
                    foreach (var err in result.Errors)
                    {
                       e =err.Description+","+e;
                    }
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response
                    {
                        Status = "Error", Message = e

                    });
                }

                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
           
              
 
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            //var user = await _userManager.FindByNameAsync(model.Username);

            //if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            //{
            //    var authClaims = new List<Claim>
            //    {
            //new Claim(ClaimTypes.Name, user.UserName),
            //new Claim("Email",user.Email),

            //    };
            //    var token = GetToken(authClaims);

            //    return Ok(new
            //    {
            //        token = new JwtSecurityTokenHandler().WriteToken(token),
            //        expiration = token.ValidTo
            //    });

            //}

            //return Unauthorized();    
            var result =  await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                var authClaims = new List<Claim>
                    {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Email",user.Email),

                    };
                var token = GetToken(authClaims);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });

            }

            else if (result.IsLockedOut)
            {
                return Ok("پنج بار ورود ناموفق قفل شد");
            }
            else
            {
                return Unauthorized(); 
            }
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }


    }
}
//[HttpPost]
//[Route("login")]
//public async Task<IActionResult> Login([FromBody] LoginModel model)
//{
//    var user = await _userManager.FindByNameAsync(model.Username);
//    if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
//    {
//        var userRoles = await _userManager.GetRolesAsync(user);

//        var authClaims = new List<Claim>
//        {
//            new Claim(ClaimTypes.Name, user.UserName),
//            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//        };

//        foreach (var userRole in userRoles)
//        {
//            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
//        }

//        var token = GetToken(authClaims);

//        return Ok(new
//        {
//            token = new JwtSecurityTokenHandler().WriteToken(token),
//            expiration = token.ValidTo
//        });
//    }
//    return Unauthorized();
//}