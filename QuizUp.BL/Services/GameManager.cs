using QuizUp.BL.Mappers;
using QuizUp.Common.Dtos;
using QuizUp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.BL.Services;

class Player
{
    public required string ID { get; set; }
    public required string Name { get; set; }
    public int Score { get; set; } = 0;
    public int LastAnsweredQuestion { get; set; } = -1;
}
class RunningGame
{
    public required int GameCode { get; set; }
    public required Guid QuizID { get; set; }
    public required string HostID { get; set; }
    public required Guid GameID { get; set; }
    public List<Player> players { get; } = [];
    public int CurrentQuestion { get; set; } = 0;
    public bool ActiveQuestion { get => TimerCancellation != null; }
    public int Answers { get; set; } = 0;
    public CancellationTokenSource? TimerCancellation { get; set; } = null;
}

internal class GameManager(IGameService gameService, IQuizService quizService) : IGameManager
{
    private readonly List<RunningGame> games = [];

    public async Task StartGame(Guid quizId, string hostId)
    {
        var result = await gameService.CreateGameAsync(quizId);
        Debug.Assert(games.All(g => g.GameID != result.Id && g.GameCode != result.Code));

        games.Add(new RunningGame
        {
            QuizID = quizId,
            HostID = hostId,
            GameCode = result.Code,
            GameID = result.Id
        });
    }

    public Task AddPlayer(string gameId, string playerID, string playerName)
    {
        var newPlayer = new Player
        {
            ID = playerID,
            Name = playerName
        };
        games.First(g => g.GameID.ToString() == gameId).players.Add(newPlayer);
        return Task.CompletedTask;
    }

    public async Task<bool> Answer(string gameId, int question, string answer, string connectionId)
    {
        var game = games.First(g => g.GameID.ToString() == gameId);
        var player = game.players.First(p => p.ID == connectionId);
        if (player.LastAnsweredQuestion < question && question == game.CurrentQuestion && game.ActiveQuestion)
        {
            player.LastAnsweredQuestion = question;
            var quiz = await quizService.GetQuizByIdAsync(game.QuizID);
            if (quiz.Questions[question].Answers.Any(a => a.AnswerText == answer && a.IsCorrect))
            {
                player.Score += 1;
            }
            game.Answers += 1;
            if (game.Answers == game.players.Count)
            {
                game.TimerCancellation?.Cancel();
                game.TimerCancellation = null;
                return true;
            }
        }
        return false;
    }

    public async Task<QuizQuestion?> NextQuestion(string gameId, string connectionId)
    {
        var game = games.First(g => g.GameID.ToString() == gameId);
        if (game.HostID != connectionId)
        {
            throw new UnauthorizedAccessException("Only the host change questions");
        }
        game.CurrentQuestion += 1;
        var quiz = await quizService.GetQuizByIdAsync(game.QuizID);
        if (quiz.Questions.Count == game.CurrentQuestion)
        {
            return null;
        }
        var question = quiz.Questions[game.CurrentQuestion];

        game.TimerCancellation = new CancellationTokenSource();
        var cancellationToken = game.TimerCancellation.Token;
        var _ = Task.Run(async () =>
        {
            await Task.Delay(question.TimeLimit * 1000, cancellationToken);
            game.TimerCancellation = null;
        });
        return QuizQuestionMapper.MapToQuizQuestion(gameId, game.CurrentQuestion, question);
    }
}
