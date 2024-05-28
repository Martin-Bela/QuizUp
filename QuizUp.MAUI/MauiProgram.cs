using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuizUp.MAUI.Resources.Constants;
using QuizUp.MAUI.Services;
using Microsoft.Maui.LifecycleEvents;

#if WINDOWS
using WinUIEx;
#endif

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
                fonts.AddFont("FontAwesome-Solid.ttf", Fonts.FontAwesome);
                fonts.AddFont("Montserrat-Bold.ttf", Fonts.Bold);
                fonts.AddFont("Montserrat-Medium.ttf", Fonts.Medium);
                fonts.AddFont("Montserrat-Regular.ttf", Fonts.Regular);
            });

        builder.ConfigureContainer(new AutofacServiceProviderFactory(), builder =>
        {
            DependencyInjection.RegisterServices(builder);
            DependencyInjection.RegisterViewModels(builder);
            DependencyInjection.RegisterViews(builder);
        });

#if DEBUG
        builder.Logging.SetMinimumLevel(LogLevel.Trace);
        builder.Logging.AddDebug();
#endif

#if WINDOWS
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddWindows(wndLifeCycleBuilder =>
            {
                wndLifeCycleBuilder.OnWindowCreated(window =>
                {
                    window.CenterOnScreen(1024,768); //Set size and center on screen using WinUIEx extension method

                    var manager = WinUIEx.WindowManager.Get(window);
                    manager.PersistenceId = "MainWindowPersistanceId"; // Remember window position and size across runs
                    manager.MinWidth = 640;
                    manager.MinHeight = 480;
                });
            });
        });
#endif

        var app = builder.Build();
        RegisterRoutes(app);
        return app;
    }

    private static void RegisterRoutes(MauiApp app)
    {
        var routingService = app.Services.GetRequiredService<IViewRoutingService>();

        foreach (var routeModel in routingService.Routes)
        {
            Routing.RegisterRoute(routeModel.Route, routeModel.ViewType);
        }
    }
}
