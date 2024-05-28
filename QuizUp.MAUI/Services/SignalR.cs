using Microsoft.AspNetCore.SignalR.Client;
using QuizUp.Common;
using QuizUp.Common.Models;
using QuizUp.MAUI.ViewModels;
using QuizUp.MAUI.Views;

namespace QuizUp.MAUI.Services;
public class SignalR : ISignalR
{

    readonly HubConnection hubConnection;

    public SignalR(IViewRoutingService routingService)
    {
        hubConnection = new HubConnectionBuilder()
                .WithUrl(AppConfig.Server.SignalRUrl)
                .AddJsonProtocol()
                .Build()
                ?? throw new Exception("Unable to connect to SignalR server!");

        hubConnection.On<QuizQuestionModel>(SignalRHubCommands.NextQuestion, question =>
        {
            var questionRoute = routingService.GetRouteByViewModel<QuestionViewModel>();
            Application.Current?.Dispatcher.Dispatch(
                async () => { await Shell.Current.GoToAsync(questionRoute, new Dictionary<string, object> { { "QuizQuestion", question } }); }
                );
        });

        hubConnection.On<int, string, string>(SignalRHubCommands.GameCreated, (passCode, gameId, quizName) =>
        {
            var startGameRoute = routingService.GetRouteByViewModel<StartGameViewModel>();
            Application.Current?.Dispatcher.Dispatch(
                async () =>
                {
                    await Shell.Current.GoToAsync(startGameRoute, new Dictionary<string, object> {
                        { "PassCode", passCode }, { "GameId", gameId }, { "QuizName", quizName }
                    });
                }
                );
        });

        hubConnection.On<string>(SignalRHubCommands.GameJoined, (gameId) =>
        {
            var gameRoute = routingService.GetRouteByView<GameIntroView>();
            Application.Current?.Dispatcher.Dispatch(
                async () => { await Shell.Current.GoToAsync(gameRoute, new Dictionary<string, object> { { "GameId", gameId } }); }
            );
        });

        hubConnection.On(SignalRHubCommands.Score, (bool quizOver, List<ScoreModel> bestPlayers) =>
        {
            var scoreRoute = routingService.GetRouteByViewModel<ScoreViewModel>();
            Application.Current?.Dispatcher.Dispatch(
                async () =>
                {
                    await Shell.Current.GoToAsync(scoreRoute, new Dictionary<string, object> { { "QuizOver", quizOver }, { "BestPlayers", bestPlayers } });
                }
            );
        });
    }

    public async Task StartAsync()
    {
        await hubConnection.StartAsync();
    }

    public async Task StopAsync()
    {
        await hubConnection.StopAsync();
    }

    public async Task CreateGameAsync(Guid quizId)
    {
        await hubConnection.InvokeAsync("CreateGame", quizId);
    }

    public async Task StartGameAsync(string gameId)
    {
        await hubConnection.InvokeAsync("NextQuestion", gameId);
    }

    public async Task JoinGameAsync(int gameCode, string playerName, Guid? playerId)
    {
        await hubConnection.InvokeAsync("JoinGame", gameCode, playerName, playerId);
    }

    public async Task AnswerQuestionAsync(string gameId, int questionId, int answer)
    {
        await hubConnection.InvokeAsync("Answer", gameId, questionId, answer);
    }

    public async Task NextQuestionAsync(string gameId)
    {
        await hubConnection.InvokeAsync("NextQuestion", gameId);
    }

    public async Task LeaveQuiz(string gameId)
    {
        await hubConnection.InvokeAsync("LeaveQuiz", gameId);
    }
}
