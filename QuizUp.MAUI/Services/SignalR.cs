using Microsoft.AspNetCore.SignalR.Client;
using QuizUp.Common.Models;

namespace QuizUp.MAUI.Services;
public class SignalR : ISignalR
{
    readonly HubConnection hubConnection;
    public event Action<string, string>? OnMessageReceived;

    public SignalR()
    {
        var baseUrl = "https://localhost";
        hubConnection = new HubConnectionBuilder()
                .WithUrl($"{baseUrl}:7126/quizHub")
                .AddJsonProtocol()
                .Build()
                ?? throw new Exception("Unable to connect to SignalR server!");

        hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                if (OnMessageReceived is not null)
                {
                    OnMessageReceived(user, message);
                }
            });

        hubConnection.On<QuizQuestion>("NextQuestion", question =>
        {
            Application.Current?.Dispatcher.Dispatch(
                async () => { await Shell.Current.GoToAsync("///Question", new Dictionary<string, object> { { "QuizQuestion", question } }); }
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

    public async Task SendMessageAsync(string user, string message)
    {
        await hubConnection.InvokeAsync("SendMessageToAll", user, message);
    }

    public async Task JoinGameAsync(string gameId)
    {
        await hubConnection.InvokeAsync("JoinQuiz", gameId);
    }

    public async Task AnswerQuestionAsync(string gameId, int questionId, string answer)
    {
        await hubConnection.InvokeAsync("Answer", gameId, questionId, answer);
    }
}
