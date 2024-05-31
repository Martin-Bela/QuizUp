using QuizUp.BL.Models.Game;
using QuizUp.Common.Models;

namespace QuizUp.BL.Services;
public interface IGameManager
{
    string GetHostID(Guid gameId);
    Func<Guid, bool, List<ScoreModel>, string, Task>? OnRoundEnded { get; set; }
    Task<Guid> CreateGameAsync(Guid quizId, string hostId);
    GameStartDataModel GetGameStartData(Guid gameId);
    Guid AddPlayer(int gameCode, string playerID, string playerName, Guid? PlayerId);
    bool Answer(Guid gameId, int question, int answer, string connectionId);
    QuizQuestionModel? NextQuestion(Guid gameId, string connectionId);
}
