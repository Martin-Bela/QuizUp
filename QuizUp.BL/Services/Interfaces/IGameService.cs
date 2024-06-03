using QuizUp.BL.Models;

namespace QuizUp.BL.Services;

public interface IGameService
{
    public Task<bool> DoesGameBelongToUser(Guid gameId, Guid userId);

    public Task<List<GameSummaryModel>> GetGamesByUserIdAsync(Guid userId);

    public Task<GameResultsModel> GetGameResultsByIdAsync(Guid gameId);

    public Task<CreateGameResultModel> CreateGameAsync(Guid quizId);

    public Task SaveGameResultsAsync(SaveGameResultsModel gameResultsModel);

    public Task DeleteGameByIdAsync(Guid gameId);
}
