using Autofac;
using IdentityModel.OidcClient;
using QuizUp.MAUI.Services;
using QuizUp.MAUI.Api;
using QuizUp.Common;
using QuizUp.MAUI.Storage;

namespace QuizUp.MAUI;

public class DependencyInjection
{
    public static void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterType<TokenHandler>().As<ITokenHandler>().InstancePerDependency();

        builder.RegisterType<HttpClientHandler>().As<HttpMessageHandler>().InstancePerDependency();

        builder.Register(context =>
        {
            var tokenHandler = context.Resolve<ITokenHandler>();
            var httpClientHandler = context.Resolve<HttpMessageHandler>();
            return new AccessTokenDelegatingHandler(tokenHandler) { InnerHandler = httpClientHandler };
        }).InstancePerDependency();

        builder.Register(context =>
        {
            var handler = context.Resolve<AccessTokenDelegatingHandler>();
            return new HttpClient(handler);
        }).InstancePerDependency();

        builder.RegisterType<UserDataStorage>().As<IUserDataStorage>().InstancePerDependency();

        builder.RegisterType<ViewRoutingService>().As<IViewRoutingService>().InstancePerDependency();
        builder.RegisterType<RunningGameService>().As<IRunningGameService>().SingleInstance();

        builder.RegisterType<UsersClient>().WithParameter("baseUrl", AppConfig.Server.BaseUrl).As<IUsersClient>().InstancePerDependency();
        builder.RegisterType<QuizzesClient>().WithParameter("baseUrl", AppConfig.Server.BaseUrl).As<IQuizzesClient>().InstancePerDependency();
        builder.RegisterType<GamesClient>().WithParameter("baseUrl", AppConfig.Server.BaseUrl).As<IGamesClient>().InstancePerDependency();

        builder.RegisterType<AuthenticationWebBrowser>().InstancePerDependency();
        builder.Register(context =>
        {
            return new OidcClient(new OidcClientOptions
            {
                Authority = AppConfig.IdentityServer.BaseUrl,
                ClientId = AppConfig.MAUI.ClientId,
                ClientSecret = AppConfig.MAUI.ClientSecret,
                RedirectUri = AppConfig.MAUI.LoginRedirectUri,
                Scope = $"{AppConfig.IdentityServer.IdentityScopes} {AppConfig.Server.ApiScopeName} {AppConfig.IdentityServer.OfflineAccessScope}",
                Browser = context.Resolve<AuthenticationWebBrowser>(),
            });
        }).InstancePerDependency();
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

        //Auth
        builder.RegisterType<ViewModels.AuthViewModel>().InstancePerDependency();
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

        //Auth
        builder.RegisterType<Views.AuthView>().InstancePerDependency();
    }
}
