using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuizUp.MAUI.Resources.Constants;
using QuizUp.MAUI.Services;

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

        var app = builder.Build();
        RegisterRoutes(app);
        return app;
    }

    private static void RegisterRoutes(MauiApp app)
    {
        var routingService = app.Services.GetRequiredService<IViewRoutingService>();

        foreach (var routeModel in routingService.Routes)
        {
            if (routeModel.Route.Count(ch => ch == '/') != 2)
            {
                Routing.RegisterRoute(routeModel.Route, routeModel.ViewType);
            }
        }
    }
}
