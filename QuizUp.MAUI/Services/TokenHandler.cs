using IdentityModel.OidcClient;
using System.IdentityModel.Tokens.Jwt;

namespace QuizUp.MAUI.Services;

public class TokenHandler(ISecureStorage secureStorage, OidcClient oidcClient) : ITokenHandler
{
    private static class StorageKeys
    {
        public const string AccessTokenKey = "AccessToken";

        public const string RefreshTokenKey = "RefreshToken";

        public const string IdentityTokenKey = "IdentityToken";
    }

    public async Task StoreAccessTokenAsync(string accessToken)
    {
        await secureStorage.SetAsync(StorageKeys.AccessTokenKey, accessToken);
    }

    public async Task StoreRefreshTokenAsync(string refreshToken)
    {
        await secureStorage.SetAsync(StorageKeys.RefreshTokenKey, refreshToken);
    }

    public async Task StoreIdentityTokenAsync(string idToken)
    {
        await secureStorage.SetAsync(StorageKeys.IdentityTokenKey, idToken);
    }

    public async Task<string?> TryGetAccessTokenAsync()
    {
        var accessToken = await secureStorage.GetAsync(StorageKeys.AccessTokenKey);
        if (accessToken != null && IsTokenValid(accessToken))
        {
            return accessToken;
        }

        var refreshToken = await secureStorage.GetAsync(StorageKeys.RefreshTokenKey);
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

        await StoreAccessTokenAsync(refreshTokenResult.AccessToken);
        await StoreRefreshTokenAsync(refreshTokenResult.RefreshToken);
        await StoreIdentityTokenAsync(refreshTokenResult.IdentityToken);

        return refreshTokenResult.AccessToken;
    }

    public Task<string?> TryGetIdentityTokenAsync()
    {
        throw new NotImplementedException();
    }

    private static bool IsTokenValid(string token)
    {
        var tokenInfo = ParseJwtToken(token);

        var expirationTime = tokenInfo.ValidTo;
        var currentTime = DateTime.UtcNow;
        var timeDifference = (expirationTime - currentTime).Seconds;

        // if the token expires in more than 30 seconds
        return timeDifference > 30;
    }

    private static JwtSecurityToken ParseJwtToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var tokenInfo = handler.ReadJwtToken(token);

        return tokenInfo;
    }
}
