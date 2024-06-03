using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Api;
using QuizUp.MAUI.Mappers;
using System.Diagnostics;

namespace QuizUp.MAUI.ViewModels;

[QueryProperty(nameof(QuizId), nameof(QuizId))]
[QueryProperty(nameof(Quiz), nameof(Quiz))]
public partial class QuizEditViewModel(
    ViewModelBase.Dependencies dependencies,
    IQuizzesClient quizzesClient
) : ViewModelBase(dependencies)
{
    public Guid QuizId { get; set; } = Guid.Empty;

    [ObservableProperty]
    public QuizDetailModel? quiz = null;

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        if (Quiz != null)
        {
            return;
        }

        if (QuizId != Guid.Empty)
        {
            Quiz = await quizzesClient.GetQuizByIdAsync(QuizId);
        }
        else
        {
            Quiz = new QuizDetailModel { Title = "", Questions = [] };
        }
    }

    [RelayCommand]
    private async Task CreateQuestionAsync()
    {
        Debug.Assert(Quiz is not null);

        List<AnswerDetailModel> answers = [];
        for (int i = 0; i < 4; i++)
        {
            answers.Add(new AnswerDetailModel { AnswerText = "", IsCorrect = false });
        }

        Quiz!.Questions.Add(new QuestionDetailModel { TimeLimit = 30, QuestionText = "", Answers = answers });
        var route = routingService.GetRouteByViewModel<QuizQuestionEditViewModel>();

        await Shell.Current.GoToAsync(route, new Dictionary<string, object> { { "Quiz", Quiz! }, { "QuestionPos", Quiz.Questions.Count - 1 } });
        reloadQuiz();
    }

    private void reloadQuiz()
    {
        var quiz = Quiz;
        Quiz = null;
        Quiz = quiz;
    }

    [RelayCommand]
    private async Task EditQuestionAsync(Guid id)
    {
        Debug.Assert(Quiz is not null);
        var questionPos = Quiz!.Questions.FindIndex(q => q.Id == id);
        var route = routingService.GetRouteByViewModel<QuizQuestionEditViewModel>();
        await Shell.Current.GoToAsync(route, new Dictionary<string, object> { { "Quiz", Quiz! }, { "QuestionPos", questionPos } });
        reloadQuiz();
    }

    [RelayCommand]
    private async Task SaveQuizAsync()
    {
        var userId = await userDataStorage.TryGetUserIdAsync();

        Debug.Assert(Quiz != null && userId != null);

        var checkResult = await CheckQuizBeforeSave(Quiz);
        if (!checkResult)
        {
            return;
        }

        if (QuizId != Guid.Empty)
        {
            await quizzesClient.EditQuizAsync(QuizId, Quiz!.MapToEditQuizModel());
        }
        else
        {
            await quizzesClient.CreateQuizAsync(new CreateQuizModel
            {
                UserId = userId ?? Guid.Empty,
                Title = Quiz!.Title,
                Questions = Quiz.Questions.Select(q => q.MapToCreateQuestionModel()).ToList()
            });
        }

        await GoBackAsync();
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        var route = routingService.GetRouteByViewModel<QuizListViewModel>();
        await Shell.Current.GoToAsync(route);
    }

    private static async Task<bool> CheckQuizBeforeSave(QuizDetailModel quiz)
    {
        if (string.IsNullOrWhiteSpace(quiz.Title))
        {
            await Shell.Current.DisplayAlert(null, "Please fill in quiz title before saving.", "Ok");
            return false;
        }

        foreach (var question in quiz.Questions)
        {
            if (string.IsNullOrWhiteSpace(question.QuestionText))
            {
                await Shell.Current.DisplayAlert(null, "Please fill in all question titles before saving.", "Ok");
                return false;
            }

            if (question.Answers.Any(a => string.IsNullOrWhiteSpace(a.AnswerText)))
            {
                await Shell.Current.DisplayAlert(null, $"Please fill in all answer texts of question {question.QuestionText} before saving.", "Ok");
                return false;
            }

            if (!question.Answers.Any(a => a.IsCorrect))
            {
                await Shell.Current.DisplayAlert(null, $"Question {question.QuestionText} must have at least one correct answer.", "Ok");
                return false;
            }
        }

        return true;
    }
}
