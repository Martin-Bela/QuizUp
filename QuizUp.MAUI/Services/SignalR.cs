using Microsoft.AspNetCore.SignalR.Client;
using QuizUp.Common.Models;
using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Services;
public class SignalR : ISignalR
{
    readonly HubConnection hubConnection;

    public SignalR(IRoutingService routingService)
    {
        var baseUrl = "https://localhost";
        hubConnection = new HubConnectionBuilder()
                .WithUrl($"{baseUrl}:7126/quizHub")
                .AddJsonProtocol()
                .Build()
                ?? throw new Exception("Unable to connect to SignalR server!");

        hubConnection.On<QuizQuestionModel>("NextQuestion", question =>
        {
            var questionRoute = routingService.GetRouteByViewModel<QuestionViewModel>();
            Application.Current?.Dispatcher.Dispatch(
                async () => { await Shell.Current.GoToAsync(questionRoute, new Dictionary<string, object> { { "QuizQuestion", question } }); }
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

    public async Task JoinGameAsync(int gameCode, string playerName)
    {
        await hubConnection.InvokeAsync("JoinQuiz", gameCode, playerName);
    }

    public async Task AnswerQuestionAsync(string gameId, int questionId, string answer)
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
