using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Api;
using System.Diagnostics;

namespace QuizUp.MAUI.ViewModels;

[QueryProperty(nameof(Quiz), nameof(Quiz))]
[QueryProperty(nameof(QuestionPos), nameof(QuestionPos))]
public partial class QuizQuestionEditViewModel(ViewModelBase.Dependencies dependencies) : ViewModelBase(dependencies)
{
    QuizDetailModel quiz = null!;

    public QuizDetailModel Quiz
    {
        get => quiz;
        set
        {
            quiz = value;
            SetQuestion();
        }
    }

    int questionPos = -1;
    public int QuestionPos
    {
        get => questionPos;
        set
        {
            questionPos = value;
            SetQuestion();
        }
    }

    [ObservableProperty]
    QuestionDetailModel? question;

    [ObservableProperty]
    public int timeLimitIndex = 2;

    partial void OnTimeLimitIndexChanged(int value) => SetTimeLimit();

    public List<string> PossibleTimeLimits => ["10s", "20s", "30s", "60s", "90s"];

    private void SetQuestion()
    {
        if (Quiz is not null && QuestionPos >= 0)
        {
            Question = null;
            Question = Quiz.Questions[QuestionPos];
            TimeLimitIndex = PossibleTimeLimits.IndexOf($"{Question.TimeLimit}s");
        }
    }

    private void SetTimeLimit()
    {
        if (Question is not null)
        {
            Question.TimeLimit = int.Parse(PossibleTimeLimits[TimeLimitIndex].Replace("s", ""));
        }
    }

    [RelayCommand]
    private async Task EditAnswerAsync(string answerPosString)
    {
        Debug.Assert(Quiz is not null);
        var answerPos = int.Parse(answerPosString);
        await Shell.Current.GoToAsync("answer", new Dictionary<string, object> { { "Quiz", Quiz }, { "QuestionPos", QuestionPos }, { "AnswerPos", answerPos } });
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..", new Dictionary<string, object> { { "Quiz", Quiz } });
    }

    [RelayCommand]
    private async Task DeleteQuestion()
    {
        Debug.Assert(Quiz != null && QuestionPos != -1);

        Quiz.Questions.RemoveAt(QuestionPos);

        await Shell.Current.GoToAsync("..", new Dictionary<string, object> { { "Quiz", Quiz } });
    }
}
