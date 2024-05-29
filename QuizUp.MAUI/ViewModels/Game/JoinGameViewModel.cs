using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Services;
using QuizUp.MAUI.Views;

namespace QuizUp.MAUI.ViewModels;

public partial class JoinGameViewModel(ViewModelBase.Dependencies dependencies, IRunningGameService gameManager) : ViewModelBase(dependencies)
{
    [ObservableProperty]
    public string gameCode = string.Empty;

    [RelayCommand]
    private async Task JoinGame()
    {
        // to-do: show some alert here -> invalid game code
        if (string.IsNullOrEmpty(GameCode) || !int.TryParse(GameCode, out var parsedGameCode))
        {
            return;
        }

        var userId = await userDataStorage.TryGetUserIdAsync();
        var userName = await userDataStorage.TryGetUserNameAsync();

        // and also here -> userId, username not found in storage -> app error
        if (userId == null || userName == null)
        {
            return;
        }

        await gameManager.JoinGameAsync(parsedGameCode, userName, userId);
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        var listViewRoute = routingService.GetRouteByView<QuizListView>();
        await Shell.Current.GoToAsync(listViewRoute);
    }
}
