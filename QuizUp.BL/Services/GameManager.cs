using QuizUp.BL.Mappers;
using QuizUp.Common.Models;
using QuizUp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.BL.Services;



internal class GameManager(IGameService gameService, IQuizService quizService) : IGameManager
{
    private readonly List<RunningGame> games = [];
    public Func<string, bool, List<ScoreModel>, Task>? OnRoundEnded { get; set; } = null;

    public async Task<(int passCode, string gameId)> CreateGameAsync(Guid quizId, string hostId)
    {
        //todo: Remove for production
        quizId = await quizService.GetFirstQuizID();

        var result = await gameService.CreateGameAsync(quizId);
        Debug.Assert(games.All(g => g.GameID != result.Id && g.GameCode != result.Code));

        games.Add(new RunningGame
        {
            QuizID = quizId,
            HostID = hostId,
            GameCode = result.Code,
            GameID = result.Id
        });

        return (result.Code, result.Id.ToString());
    }

    public Task<string> AddPlayerAsync(int gameCode, string playerID, string playerName)
    {
        var newPlayer = new Player
        {
            ID = playerID,
            Name = playerName
        };
        var game = games.First(g => g.GameCode == gameCode);
        game.Players.Add(newPlayer);
        return Task.FromResult(game.GameID.ToString());
    }

    private async Task endRound(string gameId, RunningGame game, QuizDetailModel quiz)
    {
        game.TimerCancellation = null;
        List<ScoreModel> bestPlayers = game.Players.OrderByDescending(p => p.Score).Take(5).Select(p => new ScoreModel { PlayerNickname = p.Name, Score = p.Score }).ToList() ?? [];
        var quizOver = game.CurrentQuestion + 1 == quiz.Questions.Count;
        if (quizOver)
        {
            await gameService.SaveGameResultsAsync(new SaveGameResultsModel
            {
                GameId = game.GameID,
                PlayersResults = [],
                QuestionsStatistics = []
            });
            games.Remove(game);
        }
        OnRoundEnded?.Invoke(gameId, quizOver, bestPlayers);
    }

    public async Task<bool> AnswerAsync(string gameId, int question, int answer, string connectionId)
    {
        var game = games.First(g => g.GameID.ToString() == gameId);
        var player = game.Players.First(p => p.ID == connectionId);
        if (player.LastAnsweredQuestion < question && question == game.CurrentQuestion && game.ActiveQuestion)
        {
            player.LastAnsweredQuestion = question;
            var quiz = await quizService.GetQuizByIdAsync(game.QuizID);
            if (quiz.Questions[question].Answers[answer].IsCorrect)
            {
                player.Score += 1;
            }
            game.Answers += 1;
            if (game.Answers == game.Players.Count)
            {
                game.TimerCancellation?.Cancel();
                await endRound(gameId, game, quiz);
                return true;
            }
        }
        return false;
    }

    public string GetHostID(string gameId)
    {
        return games.First(g => g.GameID.ToString() == gameId).HostID;
    }

    public async Task<QuizQuestionModel?> NextQuestionAsync(string gameId, string connectionId)
    {
        var game = games.First(g => g.GameID.ToString() == gameId);
        if (game.HostID != connectionId)
        {
            throw new UnauthorizedAccessException("Only the host change questions");
        }
        var quiz = await quizService.GetQuizByIdAsync(game.QuizID);
        game.CurrentQuestion += 1;
        if (quiz.Questions.Count == game.CurrentQuestion)
        {
            return null;
        }
        var question = quiz.Questions[game.CurrentQuestion];
        game.Answers = 0;

        game.TimerCancellation = new CancellationTokenSource();
        var cancellationToken = game.TimerCancellation.Token;
        var _ = Task.Run(async () =>
        {
            await Task.Delay(question.TimeLimit * 1000, cancellationToken);
            await endRound(gameId, game, quiz);
        });
        return QuizQuestionMapper.MapToQuizQuestion(gameId, game.CurrentQuestion, question);
    }

    private class Player
    {
        public required string ID { get; set; }
        public required string Name { get; set; }
        public int Score { get; set; } = 0;
        public int LastAnsweredQuestion { get; set; } = -1;
    }
    private class RunningGame
    {
        public required int GameCode { get; set; }
        public required Guid QuizID { get; set; }
        public required string HostID { get; set; }
        public required Guid GameID { get; set; }
        public List<Player> Players { get; } = [];
        public int CurrentQuestion { get; set; } = -1;
        public bool ActiveQuestion { get => TimerCancellation != null; }
        public int Answers { get; set; } = 0;
        public CancellationTokenSource? TimerCancellation { get; set; } = null;
    }
}
