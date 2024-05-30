using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.BL.Models.Game;
using QuizUp.MAUI.Services;

namespace QuizUp.MAUI.ViewModels;

[QueryProperty(nameof(GameStartData), nameof(GameStartData))]
public partial class StartGameViewModel(ViewModelBase.Dependencies dependencies, IRunningGameService gameService) : ViewModelBase(dependencies)
{
    [ObservableProperty]
    public GameStartDataModel gameStartData = null!;

    partial void OnGameStartDataChanged(GameStartDataModel value)
    {
        gameService.GameId = GameStartData.GameId;
    }

    [RelayCommand]
    private async Task StartGameAsync()
    {
        await gameService.StartGameAsync();
    }
}