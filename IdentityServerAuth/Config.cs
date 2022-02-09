using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerAuth
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("resource_agent_api")
                {
                    Scopes={ "agent_api.read", "agent_api.write", "agent_api.update"}
                },
                new ApiResource("resource_user_api")
                {
                    Scopes={ "user_api.read", "user_api.write", "user_api.update" }
                },
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
            {
                new ApiScope("agent_api.read","Agent Api icin okuma izni"),
                new ApiScope("agent_api.write","Agent Api icin yazma izni"),
                new ApiScope("agent_api.update","Agent Api icin guncelleme izni"),
                //
                new ApiScope("user_api.read",  "User Api icin okuma izni"),
                new ApiScope("user_api.write", "User Api icin yazma izni"),
                new ApiScope("user_api.update","User Api icin guncelleme izni")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "Country",
                    DisplayName = "Country",
                    Description = "Kullanicinin ulke bilgisi",
                    UserClaims = new[]{"country"}
                },
                new IdentityResource
                {
                    Name = "City",
                    DisplayName = "City",
                    Description = "Kullanicinin sehir bilgisi",
                    UserClaims = new[]{"city"}
                },
                new IdentityResource
                {
                    Name = "Birthdate",
                    DisplayName = "Birthdate",
                    Description = "Kullanicinin dogum tarihi bilgileri",
                    UserClaims = new[]{ "birthdate" }
                }
            };
        }

        public static IEnumerable<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "Cingozr",
                    Password = "9511",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name","Recai"),
                        new Claim("family_name","Cingoz"),
                        new Claim("country","Turkiye"),
                        new Claim("city","Istanbul"),
                        new Claim("birthdate","12-12-1990"),
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client
                {
                    ClientName = "Agent Client Uygulamasi",
                    ClientId = "agent_client",
                    ClientSecrets = new[]{new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =  { "agent_api.read", "agent_api.write", "agent_api.update" }
                },
                new Client
                {
                    ClientName = "User Client Uygulamasi",
                    ClientId = "user_client",
                    ClientSecrets = new[]{new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =  { "user_api.read", "user_api.write", "user_api.update" }
                },
                new Client
                {
                    ClientName = "Agent MVC Client Uygulamasi",
                    ClientId = "agent_mvc_client",
                    RequirePkce = false,
                    ClientSecrets = new[]{new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = new List<string>{"https://localhost:5002/signin-oidc"},
                    PostLogoutRedirectUris = new List<string>{ "https://localhost:5002/signout-callback-oidc" },
                    AllowedScopes =  {
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "agent_api.read",
                        "Country",
                        "City",
                        "Birthdate"

                    },
                    AccessTokenLifetime = DateTime.Now.AddDays(1).Second,
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)- DateTime.Now).TotalSeconds,
                    RequireConsent = true
                }
            };
        }
    }
}
