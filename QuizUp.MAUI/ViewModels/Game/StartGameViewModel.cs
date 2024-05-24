using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Services;
using System.Diagnostics;

namespace QuizUp.MAUI.ViewModels;

[QueryProperty(nameof(GameId), nameof(GameId))]
[QueryProperty(nameof(PassCode), nameof(PassCode))]
[QueryProperty(nameof(QuizName), nameof(QuizName))]
public partial class StartGameViewModel(ViewModelBase.Dependencies dependencies, IGameService gameService) : ViewModelBase(dependencies)
{
    private string? gameId;
    public string? GameId
    {
        get => gameId;
        set
        {
            if (value == null)
            {
                return;
            }
            gameService.GameId = value;
            gameId = value;
        }
    }

    [ObservableProperty]
    public int passCode;

    [ObservableProperty]
    public string? quizName;

    [RelayCommand]
    private async Task StartGameAsync()
    {
        await gameService.StartGameAsync();
    }
}