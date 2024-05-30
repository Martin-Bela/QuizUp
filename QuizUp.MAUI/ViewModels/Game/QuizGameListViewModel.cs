


using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Api;
using System.Runtime.CompilerServices;

namespace QuizUp.MAUI.ViewModels;

[QueryProperty(nameof(QuizId), nameof(QuizId))]
public partial class QuizGamesListViewModel(ViewModelBase.Dependencies dependencies, IQuizzesClient quizzesClient) : ViewModelBase(dependencies)
{
    public Guid QuizId { get; set; }

    [ObservableProperty]
    QuizGamesModel quizGames = null!;

    public override async Task OnAppearingAsync()
    {
        QuizGames = await quizzesClient.GetGamesByQuizIdAsync(QuizId);
        await base.OnAppearingAsync();
    }

    [RelayCommand]
    public async Task OpenGame(Guid gameId)
    {
        await Shell.Current.GoToAsync("game", new Dictionary<string, object> { { "GameId", gameId } });
    }
}
