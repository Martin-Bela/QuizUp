using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Services;
using QuizUp.MAUI.Services.Rest;

namespace QuizUp.MAUI.ViewModels;

[QueryProperty(nameof(QuizId), nameof(QuizId))]
public partial class QuizDetailViewModel(ViewModelBase.Dependencies dependencies, IQuizzesClient quizzesClient, IRunningGameService runningGameService) : ViewModelBase(dependencies)
{
    public Guid QuizId { get; set; }

    [ObservableProperty]
    public QuizDetailModel? quiz = null;

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        Quiz = await quizzesClient.GetQuizByIdAsync(QuizId);
    }

    [RelayCommand]
    public async Task Edit()
    {
        var route = routingService.GetRouteByViewModel<QuizEditViewModel>();
        await Shell.Current.GoToAsync(route, new Dictionary<string, object> { { "QuizId", QuizId } });
    }

    [RelayCommand]
    public async Task StartQuiz()
    {
        await runningGameService.CreateGame(QuizId);
    }
}
