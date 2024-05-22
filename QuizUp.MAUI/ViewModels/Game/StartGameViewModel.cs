using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Services;

namespace QuizUp.MAUI.ViewModels;

[QueryProperty(nameof(GameId), nameof(GameId))]
internal partial class StartGameViewModel(ViewModelBase.Dependencies dependencies, IGameService gameService) : ViewModelBase(dependencies)
{
    private readonly IGameService gameService = gameService;

    private readonly string GameId = string.Empty;

    [RelayCommand]
    private async Task StartGameAsync()
    {
        if (GameId != string.Empty)
        {
            await gameService.StartGameAsync(GameId);
        }
    }
}