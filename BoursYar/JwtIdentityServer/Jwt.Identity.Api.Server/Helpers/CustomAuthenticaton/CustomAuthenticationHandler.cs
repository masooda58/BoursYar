using Jwt.Identity.Api.Server.Resources;
using Jwt.Identity.Data.UnitOfWork;
using Jwt.Identity.Domain.Shared;
using Jwt.Identity.Domain.Token.ITokenServices;
using Jwt.Identity.Domain.User.Enum;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Jwt.Identity.Api.Server.Helpers.CustomAuthenticaton
{

    public class CustomAuthenticationHandler : AuthenticationHandler<CustomAutenthicationOptions>
    {
        private readonly ITokenValidators _tokenValidators;
        private readonly UnitOfWork _unitOfWork;


        public CustomAuthenticationHandler(IOptionsMonitor<CustomAutenthicationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ITokenValidators tokenValidators,
            UnitOfWork unitOfWork) : base(options, logger, encoder, clock)
        {
            _tokenValidators = tokenValidators;
            _unitOfWork = unitOfWork;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var reqHost = Request.Host.ToString();
                var client = await _unitOfWork.ClientRepository.GetByBaseUrlNotraking(reqHost);
                switch (client)
                {
                    case null:
                        return AuthenticateResult.Fail(MessageRes.ClientNoExist);
                   
                    case { LoginType: LoginType.Token  }:
                    {
                        return await BearerToken();
                    }
                    case { LoginType: LoginType.Cookie  }:
                    {
                        
                        return await CookieToken();
                    }
                    case { LoginType: LoginType.TokenAndCookie }:
                    {
                        var cookieAuthentication = await CookieToken();
                        return cookieAuthentication.Succeeded ? cookieAuthentication : await BearerToken();
                    }
                    default:
                        return AuthenticateResult.Fail(MessageRes.Unauthorize);
                }
            }
            catch (Exception e)
            {
                return AuthenticateResult.Fail(MessageRes.Unauthorize);
            }
        }

       


        private async Task<AuthenticateResult> CookieToken()
        {
            var isCookieToken = Request.Cookies.TryGetValue(KeyRes.Access_TokenSession, out var cookieToken);
            if (isCookieToken && !string.IsNullOrEmpty(cookieToken))
            {
                return await AuthenticateByToken(cookieToken);
            }

            return AuthenticateResult.Fail(MessageRes.Unauthorize);
        }

        private async Task<AuthenticateResult> AuthenticateByToken(string token)
        {
            var claimPrincipal = _tokenValidators.GetClaimPrincipalValidatedToken(token);
            if (claimPrincipal == null)
            {
                return AuthenticateResult.Fail(MessageRes.Unauthorize);
            }

            
            var ticket = new AuthenticationTicket(claimPrincipal, Scheme.Name);
           
            return AuthenticateResult.Success(ticket);
        }

        private async Task<AuthenticateResult> BearerToken()
        {
            string authorization = Request.Headers[HeaderNames.Authorization];
            if (string.IsNullOrEmpty(authorization))
            {
                return AuthenticateResult.Fail(MessageRes.Unauthorize);
            }

            var isBearerToken = authorization.StartsWith("bearer", StringComparison.OrdinalIgnoreCase);
            if (!isBearerToken)
            {
                return AuthenticateResult.Fail(MessageRes.Unauthorize);
            }

            var token = authorization.Substring("Bearer".Length).Trim();
            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail(MessageRes.Unauthorize);
            }

            return await AuthenticateByToken(token);
            // var isTokenValid = _tokenValidators.Validate(token);

        }
    }
}
