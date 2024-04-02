using Autofac;
using QuizUp.Server.Hubs;
using QuizUp.Server.Services;

namespace QuizUp.Server;

public class DependencyInjection
{
    public static void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterType<QuizService>().As<IQuizService>().SingleInstance();
        builder.RegisterType<QuizHub>().SingleInstance();
    }
}
