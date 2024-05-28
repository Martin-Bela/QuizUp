


using CommunityToolkit.Mvvm.ComponentModel;
using QuizUp.MAUI.Api;

namespace QuizUp.MAUI.ViewModels
{
    [QueryProperty(nameof(GameId), nameof(GameId))]
    public partial class GameResultsViewModel(ViewModelBase.Dependencies dependencies, IGamesClient gamesClient) : ViewModelBase(dependencies)
    {
        Guid GameId { get; set; }


        [ObservableProperty]
        GameResultsModel game = null!;

        public override async Task OnAppearingAsync()
        {
            Game = await gamesClient.GetGameResultsByIdAsync(GameId);
            await base.OnAppearingAsync();
        }
    }
}
