using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.Common.Models;
using QuizUp.MAUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.MAUI.ViewModels;

[QueryProperty(nameof(QuizOver), nameof(QuizOver))]
[QueryProperty(nameof(BestPlayers), nameof(BestPlayers))]
public partial class ScoreViewModel : ViewModelBase
{
    IGameService gameService;

    [ObservableProperty]
    public bool nextButtonEnabled;

    [ObservableProperty]
    public List<ScoreModel> bestPlayers = [];

    public bool quizOver;
    public bool QuizOver
    {
        get => quizOver;
        set
        {
            quizOver = value;
            if (value == true)
            {
                NextButtonEnabled = true;
            }
        }
    }

    public ScoreViewModel(ViewModelBase.Dependencies dependencies, IGameService gameService) : base(dependencies)
    {
        this.gameService = gameService;

        NextButtonEnabled = gameService.IsHost;
    }

    [RelayCommand]
    public async Task Next()
    {
        if (QuizOver)
        {
            await gameService.EndGameAsync();
            var route = gameService.IsHost ? routingService.GetRouteByViewModel<QuizListViewModel>() : routingService.GetRouteByViewModel<JoinGameViewModel>();
            await Shell.Current.GoToAsync(route);
            return;
        }
        await gameService.NextQuestion();
    }
}
