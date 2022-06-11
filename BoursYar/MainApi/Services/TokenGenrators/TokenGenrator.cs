using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityApi.Config.Extention.Models;
using IdentityApi.Models.Response;
using Microsoft.IdentityModel.Tokens;

namespace IdentityApi.Services.TokenGenrators
{
    public class TokenGenrator
    {
        private readonly JwtSettingModel _configuration;

        public TokenGenrator(JwtSettingModel config)
        {
            _configuration = config;
        }
        public UserTokenResponse GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));

            var token = new JwtSecurityToken(
                issuer: _configuration.ValidIssuer,
                audience: _configuration.ValidAudience,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new UserTokenResponse()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }
    }

}
