using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Models.Client;
using Jwt.Identity.Domain.Models.TypeEnum;

namespace Jwt.Identity.Data.IntialData
{
    public static class IntialClients
    {
        public static List<Client> GetClients() => new List<Client>
        {
            new Client()
            {
                ClientName = "Identity",
                BaseUrl = "http://localhost:3000/",
                EmailConfirmPage = "/ConfirmationEmail",
                EmailResetPage = "/ResetPassword",
                LoginType = LoginType.TokenAndCookie,
                LoginUrl = "/Login",
                SignInExternal = "/External-SIGN",
                SignOut = "/SignOut",
                Lockout = "/LouckOut"

            }



        };




    }
}
