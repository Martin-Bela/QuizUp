


using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Api;
using System.Runtime.CompilerServices;

namespace QuizUp.MAUI.ViewModels
{

    public partial class GameListViewModel(ViewModelBase.Dependencies dependencies, IGamesClient gamesClient) : ViewModelBase(dependencies)
    {
        [ObservableProperty]
        IList<GameSummaryModel> games = null!;

        public override async Task OnAppearingAsync()
        {
            Games = await gamesClient.GetGamesByUserIdAsync(await userDataStorage.TryGetUserIdAsync());
            await base.OnAppearingAsync();
        }

        [RelayCommand]
        public async Task OpenGame(Guid gameId)
        {
            var route = routingService.GetRouteByViewModel<GameResultsViewModel>();
            await Shell.Current.GoToAsync(route, new Dictionary<string, object> { { "GameId", gameId } });
        }
    }
}
