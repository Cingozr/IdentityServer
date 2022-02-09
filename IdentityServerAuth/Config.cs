using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
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
                new IdentityResources.Profile()
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
                    Password = "9511"
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
                    AllowedScopes =  {
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId
                    },
                }
            };
        }
    }
}
