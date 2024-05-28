using CommunityToolkit.Mvvm.Input;
using IdentityModel.OidcClient;
using QuizUp.MAUI.Services;
using QuizUp.MAUI.Views;

namespace QuizUp.MAUI.ViewModels;

public partial class ProfileViewModel(
    ViewModelBase.Dependencies dependencies,
    OidcClient oidcClient,
    ITokenHandler tokenHandler
) : ViewModelBase(dependencies)
{
    [RelayCommand]
    private async Task LogoutAsync()
    {
        var result = await oidcClient.LogoutAsync();

        tokenHandler.RemoveAccessToken();
        tokenHandler.RemoveRefreshToken();

        userDataStorage.RemoveUserId();
        userDataStorage.RemoveUserName();
        userDataStorage.RemoveEmail();

        var authViewRoute = routingService.GetRouteByView<AuthView>();
        await Shell.Current.GoToAsync(authViewRoute);
    }
}
