using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Api;
using QuizUp.MAUI.Views;

namespace QuizUp.MAUI.ViewModels;

public partial class QuizListViewModel(
    ViewModelBase.Dependencies dependencies,
    IQuizzesClient quizClient
) : ViewModelBase(dependencies)
{
    public async override Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        var userId = await userDataStorage.TryGetUserIdAsync();
        Quizzes = await quizClient.GetQuizzesByUserIdAsync(userId);
    }

    [ObservableProperty]
    public IList<QuizSummaryModel> quizzes = [];

    [RelayCommand]
    public async Task OpenQuiz(Guid quizId)
    {
        var route = routingService.GetRouteByView<QuizDetailView>();
        await Shell.Current.GoToAsync(route, new Dictionary<string, object> { { "QuizId", quizId } });
    }

    [RelayCommand]
    public async Task CreateQuiz()
    {
        var route = routingService.GetRouteByView<QuizEditView>();
        await Shell.Current.GoToAsync(route);
    }
}
