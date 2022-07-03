using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using IdentityApi.Config.Extention.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace IdentityApi.Services.TokenValidators
{
   public class TokenValidators
   {
       private readonly JwtSettingModel _jwtSetting;

       public TokenValidators(JwtSettingModel jwtSetting)
       {
           _jwtSetting = jwtSetting;
       }

       public bool Validate(string refreshToken)
        {
            JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
            //in tanzimat dar clint ham hast
            TokenValidationParameters validationParameters = new TokenValidationParameters()
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
              
                jwtTokenHandler.ValidateToken(refreshToken,validationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
