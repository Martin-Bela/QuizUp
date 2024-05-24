using QuizUp.MAUI.Models;
using QuizUp.MAUI.ViewModels;
using QuizUp.MAUI.Views;

namespace QuizUp.MAUI.Services;

public class ViewRoutingService : IViewRoutingService
{
    public IList<RouteModel> Routes =>
    [
        new("//quizes", typeof(QuizListView), typeof(QuizListViewModel)),
        new("//quizes/detail", typeof(QuizDetailView), typeof(QuizDetailViewModel)),
        new("//quizes/detail/edit", typeof(QuizEditView), typeof(QuizEditViewModel)),
        new("//quizes/detail/edit/question", typeof(QuizQuestionEditView), typeof(QuizQuestionEditViewModel)),
        new("//quizes/detail/edit/question/answer", typeof(QuizQuestionAnswerEditView), typeof(QuizQuestionAnswerEditViewModel)),

        new("//game/start-game", typeof(StartGameView), typeof(StartGameViewModel)),
        new("//game/join-game", typeof(JoinGameView), typeof(JoinGameViewModel)),
        new("//game/question", typeof(QuestionView), typeof(QuestionViewModel)),
        new("//game/game-intro", typeof(GameIntroView), typeof(GameIntroViewModel)),
        new("//gmae/game-results", typeof(ScoreView), typeof(ScoreViewModel)),


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

    public string GetRouteByView<TView>()
        where TView : ViewBase
        => Routes.First(route => route.ViewType == typeof(TView)).Route;
}
