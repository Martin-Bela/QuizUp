using QuizUp.Common.Models;

namespace QuizUp.BL.Services.Interfaces;

public interface IGameService
{
    public Task<List<GameSummaryModel>> GetGamesByUserIdAsync(Guid userId);

    public Task<CreateGameResultModel> CreateGameAsync(CreateGameModel gameCreateModel);

    public Task SaveGameResultsAsync(SaveGameResultsModel gameResultsModel);

    public Task<GameResultsModel> GetGameResultsAsync(Guid gameId);

    public Task DeleteGameAsync(Guid gameId);
}
