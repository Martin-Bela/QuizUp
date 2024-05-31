using QuizUp.BL.Models.Game;
using QuizUp.Common.Models;

namespace QuizUp.BL.Services;

using OnRoundEndedCallback = Func<Guid, bool, List<ScoreModel>, string,
    IList<(string ConnectionId, PlayerRoundResult result)>, Task>;

public interface IGameManager
{
    string GetHostID(Guid gameId);
    OnRoundEndedCallback? OnRoundEnded { get; set; }
    Task<Guid> CreateGameAsync(Guid quizId, string hostId);
    GameStartDataModel GetGameStartData(Guid gameId);
    Guid AddPlayer(int gameCode, string playerID, string playerName, Guid? PlayerId);
    bool Answer(Guid gameId, int question, int answer, string connectionId);
    QuizQuestionModel? NextQuestion(Guid gameId, string connectionId);
}
