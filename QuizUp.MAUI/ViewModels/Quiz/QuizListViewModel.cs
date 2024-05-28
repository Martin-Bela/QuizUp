using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Services;
using QuizUp.MAUI.Api;

namespace QuizUp.MAUI.ViewModels;

public partial class QuizListViewModel(
    ViewModelBase.Dependencies dependencies,
    IRunningGameService gameService,
    IUsersClient usersClient,
    IQuizzesClient quizClient
    ) : ViewModelBase(dependencies)
{
    public async override Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        var userId = await usersClient.GetFirstUserIdAsync();
        Quizzes = await quizClient.GetQuizzesByUserIdAsync(userId);
    }

    [ObservableProperty]
    public IList<QuizSummaryModel> quizzes = [];

    [RelayCommand]
    public async Task OpenQuiz(Guid quizId)
    {
        var route = routingService.GetRouteByViewModel<QuizDetailViewModel>();
        await Shell.Current.GoToAsync(route, new Dictionary<string, object> { { "QuizId", quizId } });
    }

    [RelayCommand]
    public async Task StartQuiz(Guid quizId)
    {
        await gameService.CreateGame(quizId);
    }

    [RelayCommand]
    public async Task CreateQuiz()
    {
        var route = routingService.GetRouteByViewModel<QuizEditViewModel>();
        await Shell.Current.GoToAsync(route);
    }
}
