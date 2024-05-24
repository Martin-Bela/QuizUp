using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Services;

namespace QuizUp.MAUI.ViewModels;

public partial class QuizListViewModel(ViewModelBase.Dependencies dependencies, IGameService gameService) : ViewModelBase(dependencies)
{
    [RelayCommand]
    private async Task StartQuiz()
    {
        await gameService.CreateGame(Guid.Empty);
    }
}
