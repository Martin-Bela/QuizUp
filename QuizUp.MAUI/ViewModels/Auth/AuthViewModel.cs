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
    [RelayCommand]
    private async Task LoginAsync()
    {
        var loginResult = await oidcClient.LoginAsync();
        if (loginResult.IsError)
        {
            await Shell.Current.DisplayAlert(null, "Error happened during login.", "Ok");
            return;
        }

        await tokenHandler.SetAccessTokenAsync(loginResult.AccessToken);
        await tokenHandler.SetRefreshTokenAsync(loginResult.RefreshToken);

        var userInfoResult = await oidcClient.GetUserInfoAsync(loginResult.AccessToken);
        if (userInfoResult.IsError)
        {
            await Shell.Current.DisplayAlert(null, "Error happened during login.", "Ok");
            return;
        }

        var userId = userInfoResult.Claims.First(claim => claim.Type == JwtClaimTypes.Subject).Value;
        var userName = userInfoResult.Claims.First(claim => claim.Type == JwtClaimTypes.Name).Value;
        var email = userInfoResult.Claims.First(claim => claim.Type == JwtClaimTypes.Email).Value;

        await userDataStorage.SetUserIdAsync(new Guid(userId));
        await userDataStorage.SetUserNameAsync(userName);
        await userDataStorage.SetEmailAsync(email);

        var quizListViewRoute = routingService.GetRouteByView<QuizListView>();
        await Shell.Current.GoToAsync(quizListViewRoute);
    }

    [RelayCommand]
    public async Task RegisterAsync()
    {
        var registerViewRoute = routingService.GetRouteByView<RegistrationView>();
        await Shell.Current.GoToAsync(registerViewRoute);
    }
}
