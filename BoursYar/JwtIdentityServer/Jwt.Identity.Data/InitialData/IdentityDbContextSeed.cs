using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jwt.Identity.Data.Context;
using Jwt.Identity.Domain.IdentityPolicy.Entity;

namespace Jwt.Identity.Data.IntialData
{
   public class IdentityDbContextSeed
    {
        public static void SeedData(IdentityContext context)
        {
            SeedInitialIdentitySetting(context);
            SeedClientInitial(context);
        }

        public static void SeedInitialIdentitySetting(IdentityContext context)
        {
            var settingExist = context.IdentitySettings.Any();
            if (!settingExist)
            {
                context.IdentitySettings.Add(new IdentitySettingPolicy());
                context.SaveChanges();
            }
        }
        public static void SeedClientInitial(IdentityContext context)
        {
            var settingExist = context.Clients.Any();
            if (!settingExist)
            {
                context.Clients.AddRange(InitialClients.GetClients());
                context.SaveChanges();
            }
        }
    }
}
