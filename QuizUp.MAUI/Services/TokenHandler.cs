using IdentityModel.OidcClient;
using Microsoft.IdentityModel.Tokens;
using QuizUp.Common;
using System.Text;

namespace QuizUp.MAUI.Services;

public class TokenHandler(OidcClient oidcClient) : ITokenHandler
{
    private static class StorageKeys
    {
        public const string AccessTokenKey = "AccessToken";

        public const string RefreshTokenKey = "RefreshToken";
    }

    public async Task SetAccessTokenAsync(string accessToken)
    {
        await SecureStorage.SetAsync(StorageKeys.AccessTokenKey, accessToken);
    }

    public async Task SetRefreshTokenAsync(string refreshToken)
    {
        await SecureStorage.SetAsync(StorageKeys.RefreshTokenKey, refreshToken);
    }

    public async Task<string?> TryGetAccessTokenAsync()
    {
        var accessToken = await SecureStorage.GetAsync(StorageKeys.AccessTokenKey);
        if (accessToken != null && IsTokenValid(accessToken))
        {
            return accessToken;
        }

        var refreshToken = await SecureStorage.GetAsync(StorageKeys.RefreshTokenKey);
        if (refreshToken == null || !IsTokenValid(refreshToken))
        {
            return null;
        }

        // try to obtain new access token with refresh token
        var refreshTokenResult = await oidcClient.RefreshTokenAsync(refreshToken);
        if (refreshTokenResult.IsError)
        {
            return null;
        }

        await SetAccessTokenAsync(refreshTokenResult.AccessToken);
        await SetRefreshTokenAsync(refreshTokenResult.RefreshToken);

        return refreshTokenResult.AccessToken;
    }

    public void RemoveAccessToken()
    {
        SecureStorage.Remove(StorageKeys.AccessTokenKey);
    }

    public void RemoveRefreshToken()
    {
        SecureStorage.Remove(StorageKeys.AccessTokenKey);
    }

    private static bool IsTokenValid(string token)
    {
        //try
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    var validationParameters = GetTokenValidationParameters();

        //    handler.ValidateToken(token, validationParameters, out var _);
        //    return true;
        //}
        //catch (Exception ex) {
        //    Console.WriteLine(ex.Message);
        //    return false;
        //}
        return true;
    }

    private static TokenValidationParameters GetTokenValidationParameters()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.IdentityServer.PublicKey));

        return new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            RequireExpirationTime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = AppConfig.IdentityServer.BaseUrl,
            IssuerSigningKey = securityKey
        };
    }
}
