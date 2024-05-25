using QuizUp.Common.Models;

namespace QuizUp.BL.Services;
public interface IGameManager
{
    string GetHostID(string gameId);
    Func<string, bool, List<ScoreModel>, Task>? OnRoundEnded { get; set; }

    Task<(int passCode, string gameId, string quizName)> CreateGameAsync(Guid quizId, string hostId);
    Task<string> AddPlayerAsync(int gameCode, string playerID, string playerName, Guid? PlayerId);
    Task<bool> AnswerAsync(string gameId, int question, int answer, string connectionId);
    Task<QuizQuestionModel?> NextQuestionAsync(string gameId, string connectionId);
}
