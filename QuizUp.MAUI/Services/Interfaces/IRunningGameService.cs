namespace QuizUp.MAUI.Services;
public interface IRunningGameService
{
    string GameId { get; set; }
    bool IsHost { get; }
    Task CreateGame(Guid quizId);
    Task StartGameAsync();
    Task JoinGameAsync(int gameCode, string playerName, Guid? playerId);
    Task EndGameAsync();
    Task AnswerQuestionAsync(int question, int answer);
    Task NextQuestion();
}
