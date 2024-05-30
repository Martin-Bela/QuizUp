namespace QuizUp.MAUI.Services;

public interface ISignalR
{
    Task StartAsync();
    Task StopAsync();
    Task JoinGameAsync(int gameCode, string playerName, Guid? playerId);
    Task AnswerQuestionAsync(Guid gameId, int question, int answer);
    Task NextQuestionAsync(Guid gameId);
    Task LeaveQuiz(Guid gameId);
    Task StartGameAsync(Guid gameId);
    Task CreateGameAsync(Guid quizId);
}
