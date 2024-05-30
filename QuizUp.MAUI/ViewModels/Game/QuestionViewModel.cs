using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.Common.Models;
using QuizUp.MAUI.Services;

namespace QuizUp.MAUI.ViewModels;

[QueryProperty(nameof(QuizQuestion), nameof(QuizQuestion))]
public partial class QuestionViewModel(ViewModelBase.Dependencies dependencies, IRunningGameService gameManager, IDispatcher dispatcher) : ViewModelBase(dependencies)
{
    QuizQuestionModel quizQuestion = new() { GameId = Guid.Empty, QuestionId = 0, Answer1 = "1", Answer2 = "2", Answer3 = "3", Answer4 = "4", Question = "QuestionPlaceholder" };

    public QuizQuestionModel QuizQuestion
    {
        get => quizQuestion;
        set
        {
            SetProperty(ref quizQuestion, value);
            SelectedAnswer = -1;
            RemainingTime = "30s";
            CreateTimer();
        }
    }

    [ObservableProperty]
    public string remainingTime = "30s";

    [ObservableProperty]
    public int selectedAnswer = -1;

    IDispatcherTimer? timer = null;

    [RelayCommand]
    async Task Answer(string answerString)
    {
        if (SelectedAnswer != -1 || gameManager.IsHost)
        {
            return;
        }
        var answer = int.Parse(answerString);
        SelectedAnswer = answer;
        await gameManager.AnswerQuestionAsync(quizQuestion.QuestionId, answer);
    }

    public void CreateTimer()
    {
        timer?.Stop();
        timer = dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += (s, e) =>
        {
            dispatcher.Dispatch(() => RemainingTime = (int.Parse(RemainingTime.TrimEnd('s')) - 1).ToString() + "s");
        };
        timer.Start();
    }
}
