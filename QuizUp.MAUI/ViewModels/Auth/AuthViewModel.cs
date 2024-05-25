using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IdentityModel.OidcClient;

namespace QuizUp.MAUI.ViewModels;

public partial class AuthViewModel(ViewModelBase.Dependencies dependencies, OidcClient oidcClient) : ViewModelBase(dependencies)
{
    [ObservableProperty]
    public string editorText = "Initial editor text";

    [RelayCommand]
    private async Task LoginAsync()
    {
        try
        {
            var loginResult = await oidcClient.LoginAsync(new LoginRequest());
            EditorText = loginResult.AccessToken;
        } catch (Exception e)
        {
            EditorText = e.Message;
        }
    }
}
