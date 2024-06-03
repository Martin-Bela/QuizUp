using Duende.IdentityServer.Models;
using IdentityModel;
using QuizUp.Common;

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
                Name = AppConfig.Server.ApiScopeName,
                DisplayName = AppConfig.Server.ApiScopeDisplayName
            }
        ];

    public static IEnumerable<Client> Clients =>
        [
            // Swagger client: interactive client using code flow + pkce
            new Client {
                ClientId = AppConfig.Server.SwaggerClientId,
                ClientSecrets = { new Secret(AppConfig.Server.SwaggerClientSecret.Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { AppConfig.Server.SwaggerLoginRedirectUri },

                AllowOfflineAccess = true,
                AllowedScopes = {
                    "openid",
                    "profile",
                    "user-info",
                    AppConfig.Server.ApiScopeName
                },

                AllowedCorsOrigins = { AppConfig.Server.BaseUrl }
            },
            // MAUI client: interactive client using code flow + pkce
            new Client
            {
                ClientId = AppConfig.MAUI.ClientId,
                ClientSecrets = { new Secret(AppConfig.MAUI.ClientSecret.Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = {
                    //"https://localhost:44300/signin-oidc",
                    AppConfig.MAUI.LoginRedirectUri
                },
                //FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = {
                    //"https://localhost:44300/signout-callback-oidc",
                    AppConfig.MAUI.LogoutRedirectUri
                },

                AllowOfflineAccess = true,
                AllowedScopes = {
                    "openid",
                    "profile",
                    "user-info",
                    AppConfig.Server.ApiScopeName
                },

                // for test purposes only -> one week
                AccessTokenLifetime = 3600 * 24 * 7
            }
        ];
}
