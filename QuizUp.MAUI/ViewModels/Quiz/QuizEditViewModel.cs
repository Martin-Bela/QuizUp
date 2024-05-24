using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Services.Rest;
using QuizUp.MAUI.Mappers;
using System.Diagnostics;

namespace QuizUp.MAUI.ViewModels;

[QueryProperty(nameof(QuizId), "QuizId")]
public partial class QuizEditViewModel(ViewModelBase.Dependencies dependencies, IQuizzesClient quizzesClient) : ViewModelBase(dependencies)
{
    public Guid QuizId { get; set; }

    [ObservableProperty]
    public QuizDetailModel? quiz = null;

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        if (QuizId is Guid id)
        {
            Quiz = await quizzesClient.GetQuizByIdAsync(id);
        }
        else
        {
            Quiz = new QuizDetailModel();
        }
    }

    [RelayCommand]
    private async Task GoToCreateQuestionAsync()
    {
        var route = routingService.GetRouteByViewModel<QuizQuestionEditViewModel>();
        await Shell.Current.GoToAsync(route);
    }

    [RelayCommand]
    private async Task SaveQuizAsync()
    {
        Debug.Assert(Quiz is not null);

        if (QuizId == Guid.Empty)
        {
            await quizzesClient.CreateQuizAsync(new CreateQuizModel
            {
                UserId = Guid.Empty,
                Title = Quiz!.Title,
                Questions = Quiz.Questions.Select(q => q.MapToCreateQuestionModel()).ToList()
            });
        }
        else
        {
            await quizzesClient.EditQuizAsync(QuizId, Quiz!.MapToEditQuizModel());
        }
        await GoBackAsync();
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
