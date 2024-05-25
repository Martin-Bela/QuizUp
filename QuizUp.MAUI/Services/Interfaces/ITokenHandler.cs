namespace QuizUp.MAUI.Services;

public interface ITokenHandler
{
    public Task StoreAccessTokenAsync(string accessToken);

    public Task StoreRefreshTokenAsync(string refreshToken);

    public Task StoreIdentityTokenAsync(string idToken);

    public Task<string?> TryGetAccessTokenAsync();

    public Task<string?> TryGetIdentityTokenAsync();
}
