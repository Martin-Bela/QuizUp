using QuizUp.MAUI.Models;
using QuizUp.MAUI.ViewModels;
using QuizUp.MAUI.Views;

namespace QuizUp.MAUI.Services;

public class ViewRoutingService : IViewRoutingService
{
    public IList<RouteModel> Routes =>
    [
        new("//auth", typeof(AuthView), typeof(AuthViewModel)),
        new("//auth/registration", typeof(RegistrationView), typeof(RegistrationViewModel)),

        new("//quizzes", typeof(QuizListView), typeof(QuizListViewModel)),
        new("//quizzes/detail", typeof(QuizDetailView), typeof(QuizDetailViewModel)),
        new("//quizzes/detail/edit", typeof(QuizEditView), typeof(QuizEditViewModel)),
        new("//quizzes/detail/edit/question", typeof(QuizQuestionEditView), typeof(QuizQuestionEditViewModel)),
        new("//quizzes/detail/edit/question/answer", typeof(QuizQuestionAnswerEditView), typeof(QuizQuestionAnswerEditViewModel)),
        new("//quizzes/detail/games", typeof(QuizGamesListView), typeof(QuizGamesListViewModel)),
        new("//quizzes/detail/games/game", typeof(GameResultsView), typeof(GameResultsViewModel)),

        new("//game/start-game", typeof(StartGameView), typeof(StartGameViewModel)),
        new("//game/join-game", typeof(JoinGameView), typeof(JoinGameViewModel)),
        new("//game/question", typeof(QuestionView), typeof(QuestionViewModel)),
        new("//game/game-intro", typeof(GameIntroView), typeof(GameIntroViewModel)),
        new("//game/game-results", typeof(ScoreView), typeof(ScoreViewModel)),

        new("//games", typeof(GameListView), typeof(GameListViewModel)),
        new("//games/detail", typeof(GameResultsView), typeof(GameResultsViewModel)),

        new("//profile", typeof(ProfileView), typeof(ProfileViewModel))
    ];

    public string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;

    public string GetRouteByView<TView>()
        where TView : ViewBase
        => Routes.First(route => route.ViewType == typeof(TView)).Route;
}
