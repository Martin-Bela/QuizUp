namespace QuizUp.MAUI.Services;

public interface ISignalR
{
    Task StartAsync();
    Task StopAsync();
    Task JoinGameAsync(string gameId);
    Task AnswerQuestionAsync(string gameId, int question, string answer);
}
