using Jwt.Identity.Api.Server.Resources;
using Jwt.Identity.Api.Server.Security;
using Jwt.Identity.Data.IntialData;
using Jwt.Identity.Domain.Interfaces.IConfirmCode;
using Jwt.Identity.Domain.Interfaces.ITokenServices;
using Jwt.Identity.Domain.Interfaces.IUserRepositories;
using Jwt.Identity.Domain.Models;
using Jwt.Identity.Domain.Models.CacheData;
using Jwt.Identity.Domain.Models.Request;
using Jwt.Identity.Domain.Models.Response;
using Jwt.Identity.Domain.Models.SettingModels;
using Jwt.Identity.Domain.Models.TypeEnum;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using static Google.Apis.Auth.GoogleJsonWebSignature;


namespace Jwt.Identity.Api.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region CTOR

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITotpCode _totpCode;
        private readonly ILogger<RegisterRequest> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IMailCode _mailCode;
        private readonly IDataProtector _protector;
        private readonly TotpSettings _totpSettings;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthClaimsGenrators _claimsGenrators;
        private readonly ITokenGenrators _tokenGenrator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AccountController(UserManager<ApplicationUser> userManager,
            ITotpCode totpCode, ILogger<RegisterRequest> logger,
            IMailCode mailCode, IMemoryCache memoryCache,
            IOptions<TotpSettings> options,
            IDataProtectionProvider dataProtectionProvider, DataProtectionPepuseString dataProtectionPepuseString,
            SignInManager<ApplicationUser> signInManager, IAuthClaimsGenrators claimsGenrators,
            ITokenGenrators tokenGenrator, IRefreshTokenRepository refreshTokenRepository)
        {
            _userManager = userManager;
            _totpCode = totpCode;
            _logger = logger;
            _mailCode = mailCode;
            _memoryCache = memoryCache;
            _signInManager = signInManager;
            _claimsGenrators = claimsGenrators;
            _tokenGenrator = tokenGenrator;
            _refreshTokenRepository = refreshTokenRepository;
            _totpSettings = options?.Value ?? new TotpSettings();
            _protector = dataProtectionProvider.CreateProtector(dataProtectionPepuseString.PhoneNoInCooki);
        }

        #endregion

        /// <summary>
        /// ثبت نام
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientName"></param>
        /// <param name="registerModel"></param>
        /// <returns> شماره موبایل یا ایمیل اگر عملیات موفقیت آمیز باشد  وگرنه خطا</returns>
        [HttpPost]
        [Route("register/{clientName}")]
        public async Task<IActionResult> Register(string clientName, [FromBody] RegisterRequest registerModel)
        {
            #region Check input
            if (string.IsNullOrEmpty(clientName))
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }
            var client = IntialClients.GetClients().SingleOrDefault(c => c.ClientName == clientName.ToUpper());
            if (client == null)
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }
            if (!ModelState.IsValid)
            {

                var errorList = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(new ResultResponse(false, errorList));
            }


            #endregion

            #region Register email And mobileNo

            var userExist = registerModel.EmailOrPhone.Contains("@")
                ? await _userManager.FindByEmailAsync(registerModel.EmailOrPhone)
                : await _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == registerModel.EmailOrPhone);

            #region Check user Exist

            if (userExist != null && registerModel.EmailOrPhone.Contains("@"))
            {
                ModelState.AddModelError(string.Empty,
                    $"ایمیل {registerModel.EmailOrPhone} قبلا در سایت ثبت نام نموده است");
                var errorList = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return Conflict(new ResultResponse(false, errorList));
            }

            if (userExist != null && !registerModel.EmailOrPhone.Contains("@"))
            {
                ModelState.AddModelError(string.Empty,
                    $"شماره {registerModel.EmailOrPhone} قبلا در سایت ثبت نام نموده است");
                var errorList = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return Conflict(new ResultResponse(false, errorList));
            }

            #endregion

            #region Create user

            var user = registerModel.EmailOrPhone.Contains("@")
                ? new ApplicationUser { UserName = registerModel.EmailOrPhone, Email = registerModel.EmailOrPhone }
                : new ApplicationUser { UserName = registerModel.EmailOrPhone, PhoneNumber = registerModel.EmailOrPhone };
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            #endregion

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "در ساختن کاربر مشکلی رخداده است.");
                var errorList = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new ResultResponse(false, errorList));
            }

            if (_userManager.Options.SignIn.RequireConfirmedAccount)
            {
                //var newUser =await _userManager.FindByNameAsync(registerModel.EmailOrPhone);

                #region Send Email Confirm

                if (registerModel.EmailOrPhone.Contains("@"))
                {
                    var (successed, errorMessage) = await _mailCode.SendMailCodeAsync(user,
                        MailTypeCode.MailAccountConfirmationCode, client.EmailConfirmPage);

                    if (successed)
                    {
                        return Ok(new ResultResponse(true, MessageRes.EmailSent, registerModel.EmailOrPhone));
                    }

                    await _userManager.DeleteAsync(user);
                    return BadRequest(new ResultResponse(false, errorMessage));
                }

                #endregion

                #region send sms confirm

                // check ip not block
                var remoteIpAddress = HttpContext!.Connection.RemoteIpAddress;

                var isIpBlock = _memoryCache.TryGetValue(remoteIpAddress!.ToString(), out TempIpBlock ipBlock);
                if (isIpBlock)
                    _memoryCache.Remove(remoteIpAddress!.ToString());
                //......
                var resualtSendTotpCodeAsync =
                    await _totpCode.SendTotpCodeAsync(registerModel.EmailOrPhone,
                        TotpTypeCode.TotpAccountConfirmationCode);
                if (resualtSendTotpCodeAsync.Successed)
                {
                    #region Set Cooki for phoneNo

                    HttpContext.Response.Cookies.Append("TempSession", _protector.Protect(registerModel.EmailOrPhone+ TotpTypeCode.TotpAccountConfirmationCode),
                        CookiesOptions.SetCookieOptions(DateTime.Now.AddSeconds(2 * _totpSettings.Step)));


                    #endregion

                    return Ok(new ResultResponse(true, MessageRes.TotpSent, registerModel.EmailOrPhone+TotpTypeCode.TotpAccountConfirmationCode));

                }

                ModelState.AddModelError(string.Empty, MessageRes.UnkonwnError);
                var errorList = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                await _userManager.DeleteAsync(user);
                return BadRequest(new ResultResponse(false, resualtSendTotpCodeAsync.ErrorMessage));


                #endregion


            }

            // اگر کاربر نیاز به کانفرم اکانت نداشت
            var resultSignIn = await _signInManager.PasswordSignInAsync(registerModel.EmailOrPhone, registerModel.Password,
                false, lockoutOnFailure: true);

            if (resultSignIn.Succeeded)
            {
                return await LoginJwt(user, client.LoginType);
            }

            return BadRequest(new ResultResponse(false, MessageRes.UnkonwnError));


            #endregion
        }

       [HttpPost("login/{clientName}")]
        public async Task<IActionResult> Login(string clientName, [FromBody] LoginRequest loginModel)
        {
            #region Check input
            if (string.IsNullOrEmpty(clientName))
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }
            var client = IntialClients.GetClients().SingleOrDefault(c => c.ClientName == clientName.ToUpper());
            if (client == null)
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }
            if (!ModelState.IsValid)
            {

                var errorList = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(new ResultResponse(false, errorList));
            }


            #endregion

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);


            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true

            var user =loginModel.EmailOrPhone.Contains("@")
                ? await _userManager.FindByEmailAsync(loginModel.EmailOrPhone)
                : await _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == loginModel.EmailOrPhone);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, " نام کاربری یا رمز عبور اشتباه است. ");

                var errorList = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return NotFound(new ResultResponse(false, errorList));
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, loginModel.Password,
                false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return await LoginJwt(user, client.LoginType);

            }
            // check locked
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return BadRequest(new ResultResponse(false, MessageRes.AccountLucked));
            }
            //check confirm
            if (user.PhoneNumberConfirmed || user.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, MessageRes.WrongUserOrPass);
                var errorList = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(new ResultResponse(false, errorList));
            }
            else
            {
                ModelState.AddModelError(string.Empty, MessageRes.AccountNotConFirm);
                var errorList = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(new ResultResponse(false, errorList));
            }


            // If we got this far, something failed, redisplay form

        }

        [HttpPost("ExternalLogin/{clientName}")]
        public async Task<ActionResult> ExternalLogin(string clientName, string token)
        {

            #region Check input
            if (string.IsNullOrEmpty(clientName))
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }
            var client = IntialClients.GetClients().SingleOrDefault(c => c.ClientName == clientName.ToUpper());
            if (client == null)
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }
            if (!ModelState.IsValid)
            {

                var errorList = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(new ResultResponse(false, errorList));
            }


            #endregion

            var payload = await ValidateAsync(token, new ValidationSettings
            {
                Audience = new[]
                {
                    "346095678950-dhuqj3ko64i5i1becqteg2v3rv9l8j6a.apps.googleusercontent.com"
                }

            });
            if (payload == null)
            {
                return BadRequest(new ResultResponse(false, MessageRes.UnkonwnError));
            }

            var user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                var newUser = new ApplicationUser { UserName = payload.Email, Email = payload.Email };
                var createdResult = await _userManager.CreateAsync(newUser);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                await _userManager.ConfirmEmailAsync(newUser, code);
                await _signInManager.SignInAsync(newUser, isPersistent: false, payload.Issuer);
                return await LoginJwt(newUser, client.LoginType);

            }

            await _signInManager.SignInAsync(user, isPersistent: false, payload.Issuer);
            return await LoginJwt(user, client.LoginType);
        }

        [HttpPost("ResetPassword/{clientName}")]
        public async Task<ActionResult> ResetPassword(string clientName, [FromBody] ForgetOrConfirmRequest input)
        {
            #region Check input
            if (string.IsNullOrEmpty(clientName))
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }
            var client = IntialClients.GetClients().SingleOrDefault(c => c.ClientName == clientName.ToUpper());
            if (client == null)
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }
            if (!ModelState.IsValid)
            {

                var errorList = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(new ResultResponse(false, errorList));
            }


            #endregion


            var userExist = input.EmailOrPhone.Contains("@")
                ?await _userManager.FindByEmailAsync(input.EmailOrPhone)
                :await _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == input.EmailOrPhone);
            if (userExist == null)
            {
                // مشخص نمی کنیم که یوزر در سایت نمی باشد
                //ModelState.AddModelError(string.Empty, $"ایمیل {input.EmailOrPhone} قبلا در سایت ثبت نام ننموده است");

                return BadRequest(new ResultResponse(false, MessageRes.UnkonwnError));
            }

            #region Send email reset

            if (input.EmailOrPhone.Contains("@"))
            {
                var (successed, errorMessage) = await _mailCode.SendMailCodeAsync(userExist,
                    MailTypeCode.MailAccountPasswordResetCode, client.EmailResetPage);

                if (successed)
                {
                    return Ok(new ResultResponse(true, input.EmailOrPhone));
                }

                return BadRequest(new ResultResponse(false, errorMessage));
            }

            #endregion

            #region Send sms rest
            var remoteIpAddress = HttpContext!.Connection.RemoteIpAddress;


            var resualtSendTotpCodeAsync =
                await _totpCode.SendTotpCodeAsync(input.EmailOrPhone,
                    TotpTypeCode.TotpAccountPasswordResetCode);
            switch (resualtSendTotpCodeAsync.Successed)
            {
                case true:

                    #region Set Cooki for phoneNo

                    HttpContext.Response.Cookies.Append("TempSession", _protector.Protect(input.EmailOrPhone + TotpTypeCode.TotpAccountPasswordResetCode),
                        CookiesOptions.SetCookieOptions(DateTime.Now.AddSeconds(2 * _totpSettings.Step)));


                    #endregion

                    return Ok(new ResultResponse(true, MessageRes.TotpSent,input.EmailOrPhone + TotpTypeCode.TotpAccountPasswordResetCode));
                default:
                    return BadRequest(new ResultResponse(false, resualtSendTotpCodeAsync.ErrorMessage));

                    #endregion
            }
        }

        [HttpPost("SendConfirmation/{clientName}")]
        public async Task<ActionResult> SendConfirmationAccount(string clientName, [FromBody] ForgetOrConfirmRequest input)
        {
            #region Check input
            if (string.IsNullOrEmpty(clientName))
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }
            var client = IntialClients.GetClients().SingleOrDefault(c => c.ClientName == clientName.ToUpper());
            if (client == null)
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }
            if (!ModelState.IsValid)
            {

                var errorList = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(new ResultResponse(false, errorList));
            }


            #endregion


            var userExist =input.EmailOrPhone.Contains("@")
                ?await _userManager.FindByEmailAsync(input.EmailOrPhone)
                :await _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == input.EmailOrPhone);
            if (userExist == null)
            {
                // مشخص نمی کنیم که یوزر در سایت نمی باشد
                //ModelState.AddModelError(string.Empty, $"ایمیل {input.EmailOrPhone} قبلا در سایت ثبت نام ننموده است");

                return BadRequest(new ResultResponse(false, MessageRes.UnkonwnError));
            }

            #region Send email reset

            if (input.EmailOrPhone.Contains("@"))
            {
                var (successed, errorMessage) = await _mailCode.SendMailCodeAsync(userExist,
                    MailTypeCode.MailAccountConfirmationCode, client.EmailConfirmPage);

                if (successed)
                {
                    return Ok(new ResultResponse(true, input.EmailOrPhone));
                }

                return BadRequest(new ResultResponse(false, errorMessage));
            }

            #endregion

            #region Send sms rest
            var remoteIpAddress = HttpContext!.Connection.RemoteIpAddress;


            var resualtSendTotpCodeAsync =
                await _totpCode.SendTotpCodeAsync(input.EmailOrPhone,
                    TotpTypeCode.TotpAccountConfirmationCode);
            switch (resualtSendTotpCodeAsync.Successed)
            {
                case true:

                    #region Set Cooki for phoneNo

                    HttpContext.Response.Cookies.Append("TempSession", _protector.Protect(input.EmailOrPhone + TotpTypeCode.TotpAccountConfirmationCode),
                        CookiesOptions.SetCookieOptions(DateTime.Now.AddSeconds(2 * _totpSettings.Step)));


                    #endregion

                    return Ok(new ResultResponse(true, MessageRes.TotpSent,input.EmailOrPhone + TotpTypeCode.TotpAccountConfirmationCode));
                default:
                    return BadRequest(new ResultResponse(false, resualtSendTotpCodeAsync.ErrorMessage));

            }
            #endregion
        }

        [HttpPost("ConfirmTotpCookie/{clientName}")]
        public async Task<ActionResult> ConfirmTotp(string clientName, [FromBody] string code)
        {
            #region Check input
            if (string.IsNullOrEmpty(clientName))
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }
            var client = IntialClients.GetClients().SingleOrDefault(c => c.ClientName == clientName.ToUpper());
            if (client == null)
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }

            #endregion

            var cookie = Request.Cookies.TryGetValue("TempSession", out string protectedPhoneAndType);
            if (!cookie)
            {

                return BadRequest(new ResultResponse(false, MessageRes.CodeExpire));

            }
            Debug.Assert(protectedPhoneAndType != null, nameof(protectedPhoneAndType) + " != null");
            var unProtectedPhoneAndType = _protector.Unprotect(protectedPhoneAndType);
            var phoneNo = unProtectedPhoneAndType.Substring(0, 12);
            var typeTotp = unProtectedPhoneAndType.Substring(12);
            var isCovertToTotpTyp = Enum.TryParse<TotpTypeCode>(typeTotp, out TotpTypeCode totpType);
            if (!isCovertToTotpTyp)
            {
                return BadRequest(new ResultResponse(false, MessageRes.CodeExpire));
            }

            var confirmResult = await _totpCode.ConfirmTotpCodeAsync(phoneNo, code, totpType);
            if (confirmResult.Successed)
            {
                var tempConfirmTotp = new TempConfirmTotp(phoneNo, totpType);
                //4 min
                _memoryCache.Set(phoneNo + typeTotp + "MobileConfirmed", tempConfirmTotp, TimeSpan.FromMinutes(4));

                #region remove coocki

                Response.Cookies.Delete("TempSession", new CookieOptions()
                {
                    Secure = true,
                });

                #endregion
                return Ok(new ResultResponse(true, phoneNo + totpType + "MobileConfirmed"));
            }


            return BadRequest(confirmResult);


        }

        [HttpPost("ConfirmTotp/{clientName}")]
        public async Task<ActionResult> ConfirmTotp(string clientName, [FromBody] TotpConfirmationCodeRequest input)
        {
            #region Check input
            if (string.IsNullOrEmpty(clientName))
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }
            var client = IntialClients.GetClients().SingleOrDefault(c => c.ClientName == clientName.ToUpper());
            if (client == null)
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }
            if (!ModelState.IsValid)
            {

                var errorList = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(new ResultResponse(false, errorList));
            }

            #endregion

            var phoneNo = input.PhoneNo;
            var typeTotp = input.TotpType;
            var isCovertToTotpTyp = Enum.TryParse<TotpTypeCode>(typeTotp, out TotpTypeCode totpType);
            if (!isCovertToTotpTyp)
            {
                return BadRequest(new ResultResponse(false, MessageRes.CodeExpire));
            }

            var confirmResult = await _totpCode.ConfirmTotpCodeAsync(phoneNo, input.Code, totpType);
            if (confirmResult.Successed)

            {
                var tempConfirmTotp = new TempConfirmTotp(phoneNo, totpType);
                _memoryCache.Set(phoneNo + totpType + "MobileConfirmed", tempConfirmTotp, TimeSpan.FromMinutes(4));

                return Ok(new ResultResponse(true, MessageRes.OKTotpInput, new { keyConfirmed = phoneNo + totpType + "MobileConfirmed" }));
            }

            return BadRequest(confirmResult);

        }
        [HttpPost("ConfirmAccountPhone/{clientName}")]
        public async Task<ActionResult> ConfirmAccountPhone(string clientName,[FromBody] string keyConfirmed)
        {
            var client = IntialClients.GetClients().SingleOrDefault(c => c.ClientName == clientName.ToUpper());
            if (client == null)
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }
            bool isConfirmed = false;
       

            isConfirmed = _memoryCache.TryGetValue(keyConfirmed, out TempConfirmTotp confirmedPhone);
            if (!isConfirmed)
            {
                return BadRequest(new ResultResponse(false, MessageRes.UnkonwnError));
            }

            if (confirmedPhone.TypeTotp == TotpTypeCode.TotpAccountConfirmationCode)
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == confirmedPhone.PhoneNo);
                if (user != null)
                {
                    var tokenPhone = await _userManager.GenerateChangePhoneNumberTokenAsync(user, confirmedPhone.PhoneNo);
                    var confirmMoleNumber = await _userManager.ChangePhoneNumberAsync(user, confirmedPhone.PhoneNo, tokenPhone);
                    //signin user    
                    await _signInManager.SignInAsync(user, false);

                    return Ok(await LoginJwt(user, client.LoginType));
                }
                return NotFound($"Unable to load user with ID '{confirmedPhone.PhoneNo}'.");
            }
            return BadRequest(new ResultResponse(false, MessageRes.UnkonwnError));

        }
        [HttpPost("ConfirmAccoutEmail/{clientName}")]
        public async Task<ActionResult> ConfirmAccoutEmail(string clientName, string code, string email)
        {
            var client = IntialClients.GetClients().SingleOrDefault(c => c.ClientName == clientName.ToUpper());
            if (client == null)
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // return NotFound();
                return NotFound($"Unable to load user with ID '{email}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);


            if (!result.Succeeded)
            {
                return BadRequest(new ResultResponse(false, MessageRes.UnkonwnError));
            }
            await _signInManager.SignInAsync(user, false);

            return Ok(await LoginJwt(user, client.LoginType));

            

        }
        [HttpPost("RestPasswordByPhone/{clientName}")]
        public async Task<ActionResult> RestPasswordByPhone(string clientName,string keyConfirmed,[FromBody]ChangePasswordRequest input)
        {
            #region Check input
            if (string.IsNullOrEmpty(clientName))
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }
            var client = IntialClients.GetClients().SingleOrDefault(c => c.ClientName == clientName.ToUpper());
            if (client == null)
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }
            if (!ModelState.IsValid)
            {

                var errorList = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(new ResultResponse(false, errorList));
            }
            #endregion
            bool isConfirmed = false;

            isConfirmed = _memoryCache.TryGetValue(keyConfirmed, out TempConfirmTotp confirmedPhone);
            if (!isConfirmed)
            {
                return BadRequest(new ResultResponse(false, MessageRes.UnkonwnError));
            }

            if (confirmedPhone.TypeTotp == TotpTypeCode.TotpAccountPasswordResetCode)
            {

                var user = await _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == confirmedPhone.PhoneNo);
                if (user != null)
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, code, input.Password);
                    //signin user    
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);

                        return Ok(await LoginJwt(user, client.LoginType));
                    }
                    var errorList = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                    return BadRequest(new ResultResponse(false, errorList));

           
                }
                return NotFound($"Unable to load user with ID '{confirmedPhone.PhoneNo}'.");
            }
            return NotFound(new ResultResponse(false,MessageRes.UnkonwnError));

        }
        [HttpPost("RestPasswordByEmail/{clientName}")]
        public async Task<ActionResult> RestPasswordByEmail(string clientName, string code, string email,[FromBody]ChangePasswordRequest input)
        {
            if (!ModelState.IsValid)
            {

                var errorList = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(new ResultResponse(false, errorList));
            }
            var client = IntialClients.GetClients().SingleOrDefault(c => c.ClientName == clientName.ToUpper());
            if (client == null)
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // return NotFound();
                return NotFound($"Unable to load user with ID '{email}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ResetPasswordAsync(user, code,input.Password);


            if (!result.Succeeded)
            {
                var errorList = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(new ResultResponse(false, errorList));
            }
            await _signInManager.SignInAsync(user, false);

            return Ok(await LoginJwt(user, client.LoginType));
            
        }
        [HttpPost("SignOut/{clientName}")]
        [Authorize]
        public async Task<ActionResult> SignOut(string clientName)
        {
            var client = IntialClients.GetClients().SingleOrDefault(c => c.ClientName == clientName.ToUpper());
            if (client == null)
            {
                return BadRequest(new ResultResponse(false, MessageRes.ClientNoExist));
            }
            await _signInManager.SignOutAsync();
            Response.Cookies.Delete("Access-TokenSession", new CookieOptions()
            {
                Secure = true,
            });
            Response.Cookies.Delete("Refresh-TokenSession", new CookieOptions()
            {
                Secure = true,
            });
            var userId = HttpContext.User.FindFirstValue("id");
            await _refreshTokenRepository.DeleteRefreshTokenByuserIdAsync(userId);
            return Ok(new ResultResponse(true, "logout"));
        }
        private async Task<ActionResult> LoginJwt(ApplicationUser user, LoginType loginType)
        {
            var authClaims = _claimsGenrators.CreatClaims(user);

            var token = _tokenGenrator.GetAccessToken(authClaims);
            await _refreshTokenRepository.DeleteRefreshTokenByuserIdAsync(user.Id);
            await _refreshTokenRepository.WritRefreshTokenAsync(user.Id, token.RefreshToken);
            // string  tokenSerialized = JsonSerializer.Serialize<UserTokenResponse>(token);

            switch (loginType)
            {
                case LoginType.TokenAndCookie:
                    Response.Cookies.Append("Access-TokenSession", token.AccessToken,
                        CookiesOptions.SetCookieOptions(token.Expiration));
                    Response.Cookies.Append("Refresh-TokenSession", token.RefreshToken,
                        CookiesOptions.SetCookieOptions(token.Expiration));
                    return Ok(new ResultResponse(true, MessageRes.UserLogin, token));
                case LoginType.Cookie:
                    Response.Cookies.Append("Access-TokenSession", token.AccessToken,
                        CookiesOptions.SetCookieOptions(token.Expiration));
                    Response.Cookies.Append("Refresh-TokenSession", token.RefreshToken,
                        CookiesOptions.SetCookieOptions(token.Expiration));
                    return Ok(new ResultResponse(true, MessageRes.UserLogin));

                case LoginType.Token:
                    return Ok(new ResultResponse(true, MessageRes.UserLogin, token));
            }

            return Ok(new ResultResponse(false, MessageRes.WrongLoginType));

        }

    }

}

