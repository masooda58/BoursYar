using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Jwt.Identity.Api.Server.Security
{
    public static class CookiesOptions
    {
        public static CookieOptions SetCookieOptions(DateTime expireDateTime)
        {
            return new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                // Domain = client.BaseUrl.ToString(),
                SameSite = SameSiteMode.None,
                Expires = expireDateTime,
            };
        }
    }
}
