using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.Common.Models;
using QuizUp.MAUI.Services;

namespace QuizUp.MAUI.ViewModels;

[QueryProperty(nameof(QuizOver), nameof(QuizOver))]
[QueryProperty(nameof(BestPlayers), nameof(BestPlayers))]
[QueryProperty(nameof(PlayerRoundResult), nameof(PlayerRoundResult))]
public partial class ScoreViewModel : ViewModelBase
{
    IRunningGameService gameService;

    [ObservableProperty]
    public bool nextButtonEnabled;

    [ObservableProperty]
    public bool previousAnswerResultEnabled;

    [ObservableProperty]
    public List<ScoreModel> bestPlayers = [];

    [ObservableProperty]
    public string nextText = "Next";

    [ObservableProperty]
    public PlayerRoundResult? playerRoundResult = null;

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
                NextText = "End quiz";
            }
        }
    }

    public ScoreViewModel(ViewModelBase.Dependencies dependencies, IRunningGameService gameService) : base(dependencies)
    {
        this.gameService = gameService;

        NextButtonEnabled = gameService.IsHost;

        PreviousAnswerResultEnabled = !gameService.IsHost;
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
