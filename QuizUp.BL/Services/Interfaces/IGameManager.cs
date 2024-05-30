using QuizUp.BL.Models.Game;
using QuizUp.Common.Models;

namespace QuizUp.BL.Services;
public interface IGameManager
{
    string GetHostID(Guid gameId);
    Func<Guid, bool, List<ScoreModel>, Task>? OnRoundEnded { get; set; }
    Task<Guid> CreateGameAsync(Guid quizId, string hostId);
    GameStartDataModel GetGameStartData(Guid gameId);
    Task<Guid> AddPlayerAsync(int gameCode, string playerID, string playerName, Guid? PlayerId);
    Task<bool> AnswerAsync(Guid gameId, int question, int answer, string connectionId);
    Task<QuizQuestionModel?> NextQuestionAsync(Guid gameId, string connectionId);
}
