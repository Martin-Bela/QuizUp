using CommunityToolkit.Mvvm.ComponentModel;
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
    [ObservableProperty]
    public string userName = string.Empty;

    [ObservableProperty]
    public string email = string.Empty;

    [RelayCommand]
    public async Task LogoutAsync()
    {
        await oidcClient.LogoutAsync();

        tokenHandler.RemoveAccessToken();
        tokenHandler.RemoveRefreshToken();

        userDataStorage.RemoveUserId();
        userDataStorage.RemoveUserName();
        userDataStorage.RemoveEmail();

        var authViewRoute = routingService.GetRouteByView<AuthView>();
        await Shell.Current.GoToAsync(authViewRoute);
    }

    public async override Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();

        UserName = await userDataStorage.TryGetUserNameAsync() ?? string.Empty;
        Email = await userDataStorage.TryGetEmailAsync() ?? string.Empty;
    }
}
