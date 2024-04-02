using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.MAUI;
internal class Dependencies
{
    public static void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterType<Services.SignalR>().As<Services.ISignalR>().InstancePerDependency();
    }

    public static void RegisterViewModels(ContainerBuilder builder)
    {
        builder.RegisterType<ViewModels.QuestionViewModel>().InstancePerDependency();
    }

    public static void RegisterViews(ContainerBuilder builder)
    {
        builder.RegisterType<Views.QuestionView>().InstancePerDependency();
        builder.RegisterType<Views.SignalRView>().InstancePerDependency();
    }
}
