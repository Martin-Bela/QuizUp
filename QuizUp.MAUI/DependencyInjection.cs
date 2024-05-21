using Autofac;

namespace QuizUp.MAUI;

public class DependencyInjection
{
    public static void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterType<Services.RoutingService>().As<Services.IRoutingService>();
        builder.RegisterType<Services.GameService>().SingleInstance();
    }

    public static void RegisterViewModels(ContainerBuilder builder)
    {
        builder.RegisterType<ViewModels.ViewModelBase.Dependencies>().SingleInstance();

        builder.RegisterType<ViewModels.JoinGameViewModel>().InstancePerDependency();
        builder.RegisterType<ViewModels.QuestionViewModel>().InstancePerDependency();

        builder.RegisterType<ViewModels.QuizDetailViewModel>().InstancePerDependency();
        builder.RegisterType<ViewModels.QuizEditViewModel>().InstancePerDependency();
        builder.RegisterType<ViewModels.QuizListViewModel>().InstancePerDependency();
        builder.RegisterType<ViewModels.QuizQuestionAnswerEditViewModel>().InstancePerDependency();
        builder.RegisterType<ViewModels.QuizQuestionEditViewModel>().InstancePerDependency();
    }

    public static void RegisterViews(ContainerBuilder builder)
    {
        builder.RegisterType<Views.JoinGameView>().InstancePerDependency();
        builder.RegisterType<Views.QuestionView>().InstancePerDependency();

        builder.RegisterType<Views.QuizDetailView>().InstancePerDependency();
        builder.RegisterType<Views.QuizEditView>().InstancePerDependency();
        builder.RegisterType<Views.QuizListView>().InstancePerDependency();
        builder.RegisterType<Views.QuizQuestionAnswerEditView>().InstancePerDependency();
        builder.RegisterType<Views.QuizQuestionEditView>().InstancePerDependency();
    }
}
