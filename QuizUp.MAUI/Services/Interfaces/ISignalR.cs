namespace QuizUp.MAUI.Services;

public interface ISignalR
{
    Task StartAsync();
    Task StopAsync();
    Task JoinGameAsync(int gameCode, string playerName, Guid? playerId);
    Task AnswerQuestionAsync(string gameId, int question, int answer);
    Task NextQuestionAsync(string gameId);
    Task LeaveQuiz(string gameId);
    Task StartGameAsync(string gameId);
    Task CreateGameAsync(Guid quizId);
}
