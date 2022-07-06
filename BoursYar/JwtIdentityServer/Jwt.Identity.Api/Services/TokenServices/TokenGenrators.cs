using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common.Jwt.Authentication;
using Jwt.Identity.Domain.Interfaces.ITokenServices;
using Jwt.Identity.Domain.Models.TokenModels;
using Microsoft.IdentityModel.Tokens;

namespace Jwt.Identity.Api.Services.TokenServices
{
    public class TokenGenrators:ITokenGenrators
    {
        private readonly JwtSettingModel _jwtSetting;

        public TokenGenrators(JwtSettingModel config)
        {
            _jwtSetting = config;
        }
        public UserTokenModel GetAccessToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret));

            var token = new JwtSecurityToken(
                issuer: _jwtSetting.ValidIssuer,
                audience: _jwtSetting.ValidAudience,
                expires: DateTime.Now.AddMinutes(_jwtSetting.AccessTokenExpirationMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            var mainToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = GetRefreshToken();
            return new UserTokenModel()
            {
                AccessToken = mainToken,
                Expiration = token.ValidTo,
                RefreshToken = refreshToken.AccessToken,
                RefreshExpiration =refreshToken.Expiration
                

            };
        }
        public TokenModel GetRefreshToken()
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.RefreshSecret));

            var token = new JwtSecurityToken(
                
                issuer: _jwtSetting.ValidIssuer,
                audience: _jwtSetting.ValidAudience,
                expires: DateTime.Now.AddMinutes(_jwtSetting.RefreshTokenExpirationminutes),
                
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new TokenModel()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),

                Expiration = token.ValidTo
            };

        }
    }
}
