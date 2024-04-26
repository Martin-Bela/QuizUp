﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.MAUI;
internal class DependencyInjection
{
    public static void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterType<Services.SignalR>().As<Services.ISignalR>().InstancePerDependency();
    }

    public static void RegisterViewModels(ContainerBuilder builder)
    {
        builder.RegisterType<ViewModels.QuestionViewModel>().InstancePerDependency();
        builder.RegisterType<ViewModels.JoinGameViewModel>().InstancePerDependency();
        builder.RegisterType<ViewModels.NextQuestionViewModel>().InstancePerDependency();
    }

    public static void RegisterViews(ContainerBuilder builder)
    {
        builder.RegisterType<Views.QuestionView>().InstancePerDependency();
        builder.RegisterType<Views.SignalRView>().InstancePerDependency();
        builder.RegisterType<Views.JoinGameView>().InstancePerDependency();
        builder.RegisterType<Views.NextQuestionView>().InstancePerDependency();
    }
}
