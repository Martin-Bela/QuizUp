using QuizUp.Common.Models;

namespace QuizUp.BL.Services;

public interface IGameService
{
    public Task<List<GameSummaryModel>> GetGamesByUserIdAsync(Guid userId);

    public Task<GameResultsModel> GetGameResultsByIdAsync(Guid gameId);

    public Task<CreateGameResultModel> CreateGameAsync(CreateGameModel gameCreateModel);

    public Task SaveGameResultsAsync(SaveGameResultsModel gameResultsModel);

    public Task DeleteGameByIdAsync(Guid gameId);
}
