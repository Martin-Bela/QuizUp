using Microsoft.EntityFrameworkCore;
using QuizUp.BL.Exceptions;
using QuizUp.BL.Mappers;
using QuizUp.Common.Models;
using QuizUp.DAL.Data;
using QuizUp.DAL.Entities;

namespace QuizUp.BL.Services;

public class GameService(ApplicationDbContext dbContext) : IGameService
{
    public async Task<List<GameSummaryModel>> GetGamesByUserIdAsync(Guid userId)
    {
        var user = await dbContext.ApplicationUsers.FindAsync(userId) ?? throw new NotFoundException($"Application user with id {userId} not found.");
        var games = await dbContext.Games
            .Where(g => g.Quiz.ApplicationUserId == userId)
            .Select(g => g.MapToGameSummaryModel())
            .ToListAsync();

        return games;
    }

    public async Task<GameResultsModel> GetGameResultsByIdAsync(Guid gameId)
    {
        var game = await dbContext.Games
            .Where(g => g.Id == gameId)
            .Include(g => g.Quiz)
            .ThenInclude(q => q.Questions)
            .ThenInclude(q => q.Answers)
            .Include(g => g.GameAnswers)
            .Include(g => g.GameApplicationUsers)
            .ThenInclude(gau => gau.ApplicationUser)
            .FirstOrDefaultAsync();

        if (game == null)
        {
            throw new NotFoundException($"Game with id ${gameId} not found.");
        }

        var gameResults = new GameResultsModel()
        {
            Title = game.Quiz.Title,
            Leaderboard = game.GameApplicationUsers
                .OrderByDescending(gau => gau.Score)
                .Select(gau => new PlayerResultModel()
                {
                    UserName = gau.ApplicationUser.UserName ?? "defaultusername",
                    Score = gau.Score
                }).ToList(),
            QuestionsStatistics = game.Quiz.Questions
                .Select(q => new QuestionStatisticsModel()
                {
                    QuestionText = q.QuestionText,
                    AnswersStatistics = game.GameAnswers
                        .Where(ga => ga.Answer.QuestionId == q.Id)
                        .Select(ga => new AnswerStatisticsModel()
                        {
                            AnswerText = ga.Answer.AnswerText,
                            AnsweredCount = ga.AnsweredCount
                        }).ToList()
                }).ToList()
        };

        return gameResults;
    }

    public async Task<CreateGameResultModel> CreateGameAsync(Guid quizId)
    {
        var newGame = new Game()
        {
            QuizId = quizId,
            IsFinished = false,
            Code = await GenerateNewGameCode()
        };

        await dbContext.Games.AddAsync(newGame);

        await dbContext.SaveChangesAsync();

        return newGame.MapToGameCreateResultModel();
    }

    public async Task SaveGameResultsAsync(SaveGameResultsModel gameResultsModel)
    {
        var game = await dbContext.Games.FindAsync(gameResultsModel.GameId);
        if (game == null)
        {
            throw new NotFoundException($"Game with id {gameResultsModel.GameId} not found");
        }

        foreach (var playerResultModel in gameResultsModel.PlayersResults)
        {
            var newGameApplicationUser = new GameApplicationUser()
            {
                GameId = gameResultsModel.GameId,
                ApplicationUserId = playerResultModel.UserId,
                Score = playerResultModel.Score
            };

            await dbContext.GameApplicationUsers.AddAsync(newGameApplicationUser);
        }

        foreach (var questionStatisticsModel in gameResultsModel.QuestionsStatistics)
        {
            foreach (var answerStatisticsModel in questionStatisticsModel.AnswersStatistics)
            {
                var newGameAnswer = new GameAnswer()
                {
                    GameId = gameResultsModel.GameId,
                    AnswerId = answerStatisticsModel.AnswerId,
                    AnsweredCount = answerStatisticsModel.AnsweredCount
                };

                await dbContext.GameAnswers.AddAsync(newGameAnswer);
            }
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteGameByIdAsync(Guid gameId)
    {
        var game = await dbContext.Games.FindAsync(gameId);
        if (game == null)
        {
            throw new NotFoundException($"Game with id ${gameId} not found");
        }

        dbContext.Games.Remove(game);

        await dbContext.SaveChangesAsync();
    }

    private async Task<int> GenerateNewGameCode()
    {
        var currentlyUsedCodes = (await dbContext.Games
            .Where(g => !g.IsFinished)
            .Select(g => g.Code)
            .ToListAsync())
            .ToHashSet();

        var random = new Random();
        // code is a six digit number
        var randomCode = random.Next(0, 999999);

        while (currentlyUsedCodes.Contains(randomCode))
        {
            randomCode = random.Next(0, 999999);
        }

        return randomCode;
    }
}
