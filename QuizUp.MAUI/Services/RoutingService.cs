using QuizUp.MAUI.Models;
using QuizUp.MAUI.ViewModels;
using QuizUp.MAUI.Views;

namespace QuizUp.MAUI.Services;

public class RoutingService : IRoutingService
{
    public IList<RouteModel> Routes =>
    [
        new("//quizes", typeof(QuizListView), typeof(QuizListViewModel)),
        new("//quizes/detail", typeof(QuizDetailView), typeof(QuizDetailViewModel)),
        new("//quizes/detail/edit", typeof(QuizEditView), typeof(QuizEditViewModel)),
        new("//quizes/detail/edit/question", typeof(QuizQuestionEditView), typeof(QuizQuestionEditViewModel)),
        new("//quizes/detail/edit/question/answer", typeof(QuizQuestionAnswerEditView), typeof(QuizQuestionAnswerEditViewModel)),

        new("//game/join-game", typeof(JoinGameView), typeof(JoinGameViewModel)),
        //new("//game/detail", typeof(GameDetailView), typeof(GameDetailViewModel)),
        //new("//game/question", typeof(GameQuestionView), typeof(GameQuestionViewModel)),
        //new("//game/leaderboard", typeof(GameLeaderboardView), typeof(GameLeaderboardViewModel)),
        //new("//game/podium", typeof(GamePodiumView), typeof(GamePodiumViewModel)),

        //new("//registration", typeof(RegistrationView), typeof(RegistrationViewModel)),
        //new("//login", typeof(LoginView), typeof(LoginViewModel)),

        //new("//profile", typeof(ProfileView), typeof(ProfileViewModel)),
        //new("//profile/edit", typeof(ProfileEditView), typeof(ProfileEditViewModel))
    ];

    public string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}
