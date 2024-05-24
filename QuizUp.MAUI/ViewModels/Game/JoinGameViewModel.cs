using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Services;

namespace QuizUp.MAUI.ViewModels;

public partial class JoinGameViewModel(ViewModelBase.Dependencies dependencies, IGameService gameManager) : ViewModelBase(dependencies)
{
    [ObservableProperty]
    public string? gameId;

    [ObservableProperty]
    public string? nickName = null;

    [ObservableProperty]
    public string quizName = "Quiz";

    [RelayCommand]
    void JoinGame()
    {
        if (string.IsNullOrWhiteSpace(GameId))
        {
            return;
        }

        if (!int.TryParse(GameId, out var gameCode))
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(NickName))
        {
            NickName = "Player";
        }

        // todo: get playerId from somewhere
        Guid? playerId = null;

        Task.Run(
            async () =>
            {
                await gameManager.JoinGameAsync(gameCode, NickName, playerId);
            }
        );
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        var route = routingService.GetRouteByViewModel<QuizListViewModel>();
        await Shell.Current.GoToAsync(route);
    }
}
