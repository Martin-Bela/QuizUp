using QuizUp.MAUI.Services;

namespace QuizUp.MAUI.ViewModels;

[QueryProperty(nameof(GameId), nameof(GameId))]

public partial class GameIntroViewModel(ViewModelBase.Dependencies dependencies, IRunningGameService gameService) : ViewModelBase(dependencies)
{
    private string? gameId;
    public string? GameId
    {
        get => gameId;
        set
        {
            if (value == null)
            {
                return;
            }
            gameId = value;
            gameService.GameId = value;
        }
    }
}
