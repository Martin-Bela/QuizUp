using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Services.Rest;
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

    private void SetQuestion()
    {
        if (Quiz is not null && QuestionPos >= 0)
        {
            Question = null;
            Question = Quiz.Questions[QuestionPos];
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
        await Shell.Current.GoToAsync("..", new Dictionary<string, object> { { "Quiz", Quiz} });
    }
}
