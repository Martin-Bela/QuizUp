﻿using Autofac;

using QuizUp.BL.Services;
using QuizUp.DAL;
using QuizUp.DAL.Data;


namespace QuizUp.BL;
public static class DependencyInjection
{
    public static void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterType<GameManager>().As<IGameManager>().InstancePerDependency();
        builder.RegisterType<GameService>().As<IGameService>().InstancePerDependency();
        builder.RegisterType<QuizService>().As<IQuizService>().InstancePerDependency();
        builder.RegisterType<ApplicationDbContext>().InstancePerDependency();
    }
}