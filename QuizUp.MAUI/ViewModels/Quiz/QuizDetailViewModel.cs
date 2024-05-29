using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Services;
using QuizUp.MAUI.Api;

namespace QuizUp.MAUI.ViewModels;

[QueryProperty(nameof(QuizId), nameof(QuizId))]
public partial class QuizDetailViewModel(
    ViewModelBase.Dependencies dependencies,
    IQuizzesClient quizzesClient,
    IRunningGameService runningGameService
) : ViewModelBase(dependencies)
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
    public async Task EditQuiz()
    {
        var route = routingService.GetRouteByViewModel<QuizEditViewModel>();
        await Shell.Current.GoToAsync(route, new Dictionary<string, object> { { "QuizId", QuizId } });
    }

    [RelayCommand]
    public async Task StartGame()
    {
        await runningGameService.CreateGame(QuizId);
    }
}
