﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace QuizUp.MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });


        builder.ConfigureContainer(new AutofacServiceProviderFactory(), builder =>
        {
            Dependencies.RegisterServices(builder);
            Dependencies.RegisterViewModels(builder);
            Dependencies.RegisterViews(builder);
        });


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
