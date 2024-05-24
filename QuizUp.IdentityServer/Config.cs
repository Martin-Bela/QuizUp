using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace QuizUp.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource()
            {
                Name = "user-info",
                DisplayName = "User Info",
                UserClaims =
                [
                    JwtClaimTypes.Email,
                ]
            }
        ];

    public static IEnumerable<ApiScope> ApiScopes =>
        [
            new ApiScope() 
            {
                Name = "quizup-api",
                DisplayName = "QuizUp API"
            }
        ];

    public static IEnumerable<Client> Clients =>
        [
            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "quizup-mobile",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:44300/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { 
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "user-info",
                    "quizup-api"
                }
            },
        ];
}
