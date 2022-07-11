using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BoursYar.Authorization.repositories;
using Common.Jwt.Authentication;
using Jwt.Identity.Api.Services.TokenServices;
using Jwt.Identity.Test.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Jwt.Identity.Test
{
    public class TokenGenratorsAndValidationTest
    {
        [Fact]
        public async Task TokenGenrators_CreatToken_returnJwtToken()
        {
            
            var tokenGenrators =ApiHelper.CreaTokenGenrators();
            var tokentValidators =ApiHelper.CreaTokenValidators();
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "user.UserName"),
                new Claim(ClaimStore.BoursYarAccess,"x"),
                new Claim(ClaimStore.BoursYarAccess,"y"),
                new Claim("id","user.Id"),

            };
            //Act
            var genrateResult = tokenGenrators.GetAccessToken(authClaims);
            var validResult = tokentValidators.Validate(genrateResult.RefreshToken);
            //Assert
            Assert.True(validResult);

        }

    }
}
