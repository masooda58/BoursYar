using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Common.Jwt.Authentication;
using Jwt.Identity.Domain.Token.ITokenServices;
using Microsoft.IdentityModel.Tokens;

namespace Jwt.Identity.Api.Server.Services.TokenServices
{
    public class TokenValidators : ITokenValidators
    {
        private readonly JwtSettingModel _jwtSetting;

        public TokenValidators(JwtSettingModel jwtSetting)
        {
            _jwtSetting = jwtSetting;
        }

        public bool Validate(string refreshToken)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            //in tanzimat dar clint ham hast
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = _jwtSetting.ValidAudience,
                ValidIssuer = _jwtSetting.ValidIssuer,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero,


                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.RefreshSecret))
            };
            try
            {
                jwtTokenHandler.ValidateToken(refreshToken, validationParameters, out var validatedToken);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}