using System.Security.Claims;

namespace QuizUp.MAUI.Services;

public interface ITokenHandler
{
    public Task SetAccessTokenAsync(string accessToken);

    public Task SetRefreshTokenAsync(string refreshToken);

    public Task<string?> TryGetAccessTokenAsync();
}
