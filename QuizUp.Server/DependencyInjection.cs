using Autofac;
using QuizUp.Server.Hubs;

namespace QuizUp.Server;

public static class DependencyInjection
{
    public static void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterType<QuizHub>().SingleInstance();

        BL.DependencyInjection.RegisterServices(builder);
    }
}
