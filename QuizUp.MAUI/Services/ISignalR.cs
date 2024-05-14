namespace QuizUp.MAUI.Services;

public interface ISignalR
{
    event Action<string, string>? OnMessageReceived;
    Task StartAsync();
    Task StopAsync();
    Task SendMessageAsync(string user, string message);
    Task JoinGameAsync(string gameId);
    Task AnswerQuestionAsync(string gameId, int question, string answer);
}
