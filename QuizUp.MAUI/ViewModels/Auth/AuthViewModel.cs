using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IdentityModel.OidcClient;
using QuizUp.MAUI.Services;

namespace QuizUp.MAUI.ViewModels;

public partial class AuthViewModel(ViewModelBase.Dependencies dependencies, OidcClient oidcClient, ITokenHandler tokenHandler) : ViewModelBase(dependencies)
{
    [ObservableProperty]
    public string editorText = "Initial editor text";

    [RelayCommand]
    private async Task LoginAsync()
    {
        var loginResult = await oidcClient.LoginAsync();
        if (loginResult.IsError)
        {
            EditorText = loginResult.ErrorDescription;
            return;
        }

        await tokenHandler.StoreAccessTokenAsync(loginResult.AccessToken);
        await tokenHandler.StoreRefreshTokenAsync(loginResult.RefreshToken);
        await tokenHandler.StoreIdentityTokenAsync(loginResult.IdentityToken);

        EditorText = loginResult.AccessToken;
    }
}
