using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Api;
using QuizUp.MAUI.Views;

namespace QuizUp.MAUI.ViewModels;

public partial class RegistrationViewModel(
    ViewModelBase.Dependencies dependencies,
    IUsersClient usersClient
) : ViewModelBase(dependencies)
{
    [ObservableProperty]
    public string email = string.Empty;

    [ObservableProperty]
    public string userName = string.Empty;

    [ObservableProperty]
    public string password = string.Empty;

    [ObservableProperty]
    public string passwordConfirmation = string.Empty;

    [RelayCommand]
    private async Task RegisterAsync()
    {
        var checkResult = await CheckInputs();
        if (!checkResult)
        {
            return;
        }

        try
        {
            await usersClient.RegisterUserAsync(new CreateUserModel()
            {
                Email = Email,
                Username = UserName,
                Password = Password
            });
        } catch
        {
            await Shell.Current.DisplayAlert(null, "Error happened during registration.", "Ok");
        }

        var authViewRoute = routingService.GetRouteByView<AuthView>();
        await Shell.Current.GoToAsync(authViewRoute);
    }

    private async Task<bool> CheckInputs()
    {
        if (!Email.Contains('@') || !Email.Contains('.'))
        {
            await Shell.Current.DisplayAlert(null, "Please enter a valid email.", "Ok");
            return false;
        }

        if (UserName.Length < 3)
        {
            await Shell.Current.DisplayAlert(null, "Username must have at least 3 characters.", "Ok");
            return false;
        }

        if (Password.Length < 10 ||
            !Password.Any(c => char.IsUpper(c)) || // at least one upper case letter
            !Password.Any(c => char.IsDigit(c)) || // at least one digit
            !Password.Any(c => char.IsLetterOrDigit(c)) // at leat one special character
        )
        {
            await Shell.Current.DisplayAlert(
                null, 
                "Password must be at least 10 characters long and must contain at least one upper case letter, one digit and one special character.",
                "Ok"
            );
            return false;
        }

        if (Password != PasswordConfirmation)
        {
            await Shell.Current.DisplayAlert(null, "Passwords do not match.", "Ok");
            return false;
        }

        return true;
    }
}
