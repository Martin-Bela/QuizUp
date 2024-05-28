
namespace QuizUp.MAUI.Storage;

public class UserDataStorage : IUserDataStorage
{
    private static class StorageKeys
    {
        public const string UserIdKey = "UserId";

        public const string UserNameKey = "UserName";

        public const string EmailKey = "Email";
    }

    public async Task SetUserIdAsync(Guid userId)
    {
        await SecureStorage.SetAsync(StorageKeys.UserIdKey, userId.ToString());
    }

    public async Task SetUserNameAsync(string userName)
    {
        await SecureStorage.SetAsync(StorageKeys.UserNameKey, userName);
    }

    public async Task SetEmailAsync(string email)
    {
        await SecureStorage.SetAsync(StorageKeys.EmailKey, email);
    }

    public async Task<Guid?> TryGetUserIdAsync()
    {
        var userId = await SecureStorage.GetAsync(StorageKeys.UserIdKey);

        return userId == null ? null : new Guid(userId);
    }

    public async Task<string?> TryGetUserNameAsync()
    {
        return await SecureStorage.GetAsync(StorageKeys.UserNameKey);
    }

    public async Task<string?> TryGetEmailAsync()
    {
        return await SecureStorage.GetAsync(StorageKeys.EmailKey);
    }

    public void RemoveUserId()
    {
        SecureStorage.Remove(StorageKeys.UserIdKey);
    }

    public void RemoveUserName()
    {
        SecureStorage.Remove(StorageKeys.UserNameKey);
    }

    public void RemoveEmail()
    {
        SecureStorage.Remove(StorageKeys.EmailKey);
    }
}
