namespace QuizUp.MAUI.Services;

public interface ISignalR
{
    Task StartAsync();
    Task StopAsync();
    Task JoinGameAsync(int gameCode, string playerName);
    Task AnswerQuestionAsync(string gameId, int question, string answer);
}
