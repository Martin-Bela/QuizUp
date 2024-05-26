using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IdentityModel;
using IdentityModel.OidcClient;
using QuizUp.MAUI.Services;
using QuizUp.MAUI.Views;

namespace QuizUp.MAUI.ViewModels;

public partial class AuthViewModel(
    ViewModelBase.Dependencies dependencies,
    OidcClient oidcClient,
    ITokenHandler tokenHandler
) : ViewModelBase(dependencies)
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

        await tokenHandler.SetAccessTokenAsync(loginResult.AccessToken);
        await tokenHandler.SetRefreshTokenAsync(loginResult.RefreshToken);

        var userClaims = (await oidcClient.GetUserInfoAsync(loginResult.AccessToken)).Claims;

        var userId = userClaims.First(claim => claim.Type == JwtClaimTypes.Subject).Value;
        var userName = userClaims.First(claim => claim.Type == JwtClaimTypes.Name).Value;
        var email = userClaims.First(claim => claim.Type == JwtClaimTypes.Email).Value;

        await userDataStorage.SetUserIdAsync(new Guid(userId));
        await userDataStorage.SetUserNameAsync(userName);
        await userDataStorage.SetEmailAsync(email);

        EditorText = userId + "\n" + userName + "\n" + email;

        var quizListViewRoute = routingService.GetRouteByView<QuizListView>();
        await Shell.Current.GoToAsync(quizListViewRoute);
    }
}
