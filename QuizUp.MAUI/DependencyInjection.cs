using Autofac;
using QuizUp.MAUI.Services;
using QuizUp.MAUI.Services.Rest;

namespace QuizUp.MAUI;

public class DependencyInjection
{
    private const string restUrl = "https://localhost:7126/";
    public static void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterType<HttpClient>().As<HttpClient>().InstancePerDependency();

        builder.RegisterType<ViewRoutingService>().As<IViewRoutingService>().InstancePerDependency();
        builder.RegisterType<RunningGameService>().As<IRunningGameService>().SingleInstance();

        builder.RegisterType<UsersClient>().WithParameter("baseUrl", restUrl).As<IUsersClient>().InstancePerDependency();
        builder.RegisterType<QuizzesClient>().WithParameter("baseUrl", restUrl).As<IQuizzesClient>().InstancePerDependency();
        builder.RegisterType<GamesClient>().WithParameter("baseUrl", restUrl).As<IGamesClient>().InstancePerDependency();
    }

    public static void RegisterViewModels(ContainerBuilder builder)
    {
        builder.RegisterType<ViewModels.ViewModelBase.Dependencies>().SingleInstance();

        //Game
        builder.RegisterType<ViewModels.StartGameViewModel>().InstancePerDependency();
        builder.RegisterType<ViewModels.JoinGameViewModel>().InstancePerDependency();
        builder.RegisterType<ViewModels.GameIntroViewModel>().InstancePerDependency();
        builder.RegisterType<ViewModels.QuestionViewModel>().InstancePerDependency();
        builder.RegisterType<ViewModels.ScoreViewModel>().InstancePerDependency();


        //Quiz
        builder.RegisterType<ViewModels.QuizDetailViewModel>().InstancePerDependency();
        builder.RegisterType<ViewModels.QuizEditViewModel>().InstancePerDependency();
        builder.RegisterType<ViewModels.QuizListViewModel>().InstancePerDependency();
        builder.RegisterType<ViewModels.QuizQuestionAnswerEditViewModel>().InstancePerDependency();
        builder.RegisterType<ViewModels.QuizQuestionEditViewModel>().InstancePerDependency();
    }

    public static void RegisterViews(ContainerBuilder builder)
    {
        //Game
        builder.RegisterType<Views.StartGameView>().InstancePerDependency();
        builder.RegisterType<Views.JoinGameView>().InstancePerDependency();
        builder.RegisterType<Views.GameIntroView>().InstancePerDependency();
        builder.RegisterType<Views.QuestionView>().InstancePerDependency();
        builder.RegisterType<Views.ScoreView>().InstancePerDependency();

        //Quiz
        builder.RegisterType<Views.QuizDetailView>().InstancePerDependency();
        builder.RegisterType<Views.QuizEditView>().InstancePerDependency();
        builder.RegisterType<Views.QuizListView>().InstancePerDependency();
        builder.RegisterType<Views.QuizQuestionAnswerEditView>().InstancePerDependency();
        builder.RegisterType<Views.QuizQuestionEditView>().InstancePerDependency();
    }
}
