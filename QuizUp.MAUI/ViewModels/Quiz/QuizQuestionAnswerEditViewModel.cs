using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Api;

namespace QuizUp.MAUI.ViewModels;

[QueryProperty(nameof(Quiz), nameof(Quiz))]
[QueryProperty(nameof(QuestionPos), nameof(QuestionPos))]
[QueryProperty(nameof(AnswerPos), nameof(AnswerPos))]
public partial class QuizQuestionAnswerEditViewModel(ViewModelBase.Dependencies dependencies) : ViewModelBase(dependencies)
{
    QuizDetailModel quiz = null!;
    public QuizDetailModel Quiz
    {
        get => quiz;
        set
        {
            quiz = value;
            SetAnswer();
        }
    }

    int questionPos = -1;
    public int QuestionPos
    {
        get => questionPos;
        set
        {
            questionPos = value;
            SetAnswer();
        }
    }

    [ObservableProperty]
    int answerPos = -1;

    partial void OnAnswerPosChanged(int value) => SetAnswer();

    [ObservableProperty]
    public AnswerDetailModel? answer;

    private void SetAnswer()
    {
        if (Quiz is not null && QuestionPos >= 0 && AnswerPos >= 0)
        {
            Answer = Quiz.Questions[QuestionPos].Answers[AnswerPos];
        }
    }

    [RelayCommand]
    private async Task SaveAnswerAsync()
    {
        await Shell.Current.GoToAsync("../", new Dictionary<string, object> { { "Quiz", Quiz } });
    }
}
