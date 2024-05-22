using QuizUp.Common.Models;

namespace QuizUp.BL.Services;

public interface IGameService
{
    public Task<List<GameSummaryModel>> GetGamesByUserIdAsync(Guid userId);

    public Task<GameResultsModel> GetGameResultsByIdAsync(Guid gameId);

    public Task<CreateGameResultModel> CreateGameAsync(Guid quizId);

    public Task SaveGameResultsAsync(Guid gameId, SaveGameResultsModel gameResultsModel);

    public Task DeleteGameByIdAsync(Guid gameId);
}
