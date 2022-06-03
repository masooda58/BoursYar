using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace Identity
{
    public class Config
    {
         public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                // You may add other identity resources like address,phone... etc
                //new IdentityResources.Address()
            };
        }

        // Block 1: All APIs, I want to protect in my system
        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("identity.api", "Identity API"),
                new ApiResource("test.api","Test API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                //Block 2:  MVC client using hybrid flow
                new Client
                {
                    ClientId = "webclient",
                    ClientName = "Web Client",
                    RequireConsent = false,
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    RedirectUris = { "https://localhost:5002/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:5002/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "identity.api","test.api" }
                },

                //Block 3: SPA client using Code flow
                new Client
                {
                    ClientId = "spaclient",
                    ClientName = "SPA Client",
                    ClientUri = "https://localhost:44356/",
                    RequireConsent = false,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =
                    {
                       
                        "https://localhost:44356/signin-oidc" 
                    },

                    PostLogoutRedirectUris = {  "https://localhost:44356/signout-oidc" },
                    AllowedCorsOrigins = { "https://localhost:44356" },

                    AllowedScopes = { "openid", "profile" }
                }
            };
        }
    }
}
