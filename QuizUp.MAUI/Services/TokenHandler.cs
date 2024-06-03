using IdentityModel;
using IdentityModel.OidcClient;
using Microsoft.IdentityModel.Tokens;
using QuizUp.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
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
        if (refreshToken == null)
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
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var validationParameters = GetTokenValidationParameters();

            handler.ValidateToken(token, validationParameters, out var validationResult);

            var timeDifference = (validationResult.ValidTo - DateTime.UtcNow).TotalSeconds;

            // if access token expires in less than 30 seconds, use refresh token to get a new access token
            return timeDifference > 30;
        }
        catch
        {
            return false;
        }
    }

    private static TokenValidationParameters GetTokenValidationParameters()
    {
        var rsa = RSA.Create();
        rsa.ImportParameters(new RSAParameters
        {
            Modulus = Base64UrlEncoder.DecodeBytes(AppConfig.IdentityServer.PublicKeyModulus),
            Exponent = Base64UrlEncoder.DecodeBytes(AppConfig.IdentityServer.PublicKeyExponent)
        });

        var securityKey = new RsaSecurityKey(rsa)
        {
            KeyId = AppConfig.IdentityServer.PublicKeyId
        };

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
