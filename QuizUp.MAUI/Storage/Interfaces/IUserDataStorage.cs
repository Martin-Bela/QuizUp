namespace QuizUp.MAUI.Storage;

public interface IUserDataStorage
{
    Task SetUserIdAsync(Guid userId);

    Task SetUserNameAsync(string userName);

    Task SetEmailAsync(string email);

    Task<Guid?> TryGetUserIdAsync();

    Task<string?> TryGetUserNameAsync();

    Task<string?> TryGetEmailAsync();
}
