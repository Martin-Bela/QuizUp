using QuizUp.MAUI.Services;
using QuizUp.MAUI.Views;

namespace QuizUp.MAUI;

public partial class App : Application
{
    private readonly IViewRoutingService viewRoutingService;
    private readonly ITokenHandler tokenHandler;

    public App(IViewRoutingService viewRoutingService, ITokenHandler tokenHandler)
    {
        this.viewRoutingService = viewRoutingService;
        this.tokenHandler = tokenHandler;

        InitializeComponent();

        MainPage = new AppShell();
    }

    protected override async void OnStart()
    {
        base.OnStart();

        var accessToken = await tokenHandler.TryGetAccessTokenAsync();

        if (accessToken == null) 
        {
            var authViewRoute = viewRoutingService.GetRouteByView<AuthView>();
            await Shell.Current.GoToAsync(authViewRoute);
        }
        else
        {
            var quizListViewRoute = viewRoutingService.GetRouteByView<QuizListView>();
            await Shell.Current.GoToAsync(quizListViewRoute);
        }
    }
}
