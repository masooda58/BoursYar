using IdentityApi.Config.Extention.Models;
using IdentityApi.Models.Response;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityApi.Services.TokenGenrators
{
    public class TokenGenrator : ITokenGenrators
    {
        private readonly JwtSettingModel _jwtSetting;

        public TokenGenrator(JwtSettingModel config)
        {
            _jwtSetting = config;
        }
        public UserTokenResponse GetAccessToken(List<Claim> authClaims)
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
            return new UserTokenResponse()
            {
                AccessToken = mainToken,
                Expiration = token.ValidTo,
                RefreshToken = refreshToken.Token,
                RefreshExpiration = refreshToken.Expiration


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
                Token = new JwtSecurityTokenHandler().WriteToken(token),

                Expiration = token.ValidTo
            };

        }
    }

    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

    }
}
