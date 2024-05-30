using QuizUp.BL.Mappers;
using QuizUp.BL.Models;
using QuizUp.Common.Models;
using System.Diagnostics;

namespace QuizUp.BL.Services;

internal class GameManager(IGameService gameService, IQuizService quizService) : IGameManager
{
    private readonly List<RunningGame> games = [];
    public Func<string, bool, List<ScoreModel>, Task>? OnRoundEnded { get; set; } = null;

    public async Task<(int passCode, string gameId, string quizName)> CreateGameAsync(Guid quizId, string hostId)
    {
        var result = await gameService.CreateGameAsync(quizId);
        Debug.Assert(games.All(g => g.GameID != result.Id && g.GameCode != result.Code));

        var quiz = await quizService.GetQuizByIdAsync(quizId);

        games.Add(new RunningGame
        {
            Quiz = quiz,
            HostID = hostId,
            GameCode = result.Code,
            GameID = result.Id
        });

        return (result.Code, result.Id.ToString(), result.Title);
    }

    public Task<string> AddPlayerAsync(int gameCode, string connectionId, string playerName, Guid? PlayerId)
    {
        var newPlayer = new Player
        {
            ConnectionId = connectionId,
            Name = playerName,
            PlayerId = PlayerId
        };
        var game = games.First(g => g.GameCode == gameCode);
        game.Players.Add(newPlayer);
        return Task.FromResult(game.GameID.ToString());
    }

    private async Task EndRoundAsync(string gameId, RunningGame game, QuizDetailModel quiz)
    {
        game.TimerCancellation = null;
        List<ScoreModel> bestPlayers = game.Players.OrderByDescending(p => p.Score).Take(5).Select(p => new ScoreModel { PlayerNickname = p.Name, Score = p.Score }).ToList() ?? [];
        var quizOver = game.CurrentQuestion + 1 == quiz.Questions.Count;
        if (quizOver)
        {
            var playersResults = game.Players
                .Where(p => p.PlayerId is not null)
                .Select(p => new SavePlayerResultModel { UserId = p.PlayerId!.Value, Score = p.Score })
                .ToList();

            await gameService.SaveGameResultsAsync(new SaveGameResultsModel
            {
                GameId = game.GameID,
                PlayersResults = playersResults,
                QuestionsStatistics = game.QuestionsStatistics
            });
            games.Remove(game);
        }
        OnRoundEnded?.Invoke(gameId, quizOver, bestPlayers);
    }

    public async Task<bool> AnswerAsync(string gameId, int question, int answer, string connectionId)
    {
        var game = games.First(g => g.GameID.ToString() == gameId);
        var player = game.Players.First(p => p.ConnectionId == connectionId);
        if (player.LastAnsweredQuestion < question && question == game.CurrentQuestion && game.IsQuestionActive())
        {
            player.LastAnsweredQuestion = question;
            var quiz = game.Quiz;
            game.QuestionsStatistics[question].AnswersStatistics[answer].AnsweredCount += 1;
            if (quiz.Questions[question].Answers[answer].IsCorrect)
            {
                player.Score += ComputeScore(game.QuestionStartTime, DateTime.Now, quiz.Questions[question].TimeLimit);
            }
            game.Answers += 1;
            if (game.Answers == game.Players.Count)
            {
                game.TimerCancellation?.Cancel();
                await EndRoundAsync(gameId, game, quiz);
                return true;
            }
        }
        return false;
    }

    public string GetHostID(string gameId)
    {
        return games.First(g => g.GameID.ToString() == gameId).HostID;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<QuizQuestionModel?> NextQuestionAsync(string gameId, string connectionId)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        var game = games.First(g => g.GameID.ToString() == gameId);
        var quiz = game.Quiz;
        if (game.HostID != connectionId)
        {
            throw new UnauthorizedAccessException("Only the host change questions");
        }

        game.CurrentQuestion += 1;
        if (quiz.Questions.Count == game.CurrentQuestion)
        {
            return null;
        }

        var question = quiz.Questions[game.CurrentQuestion];
        game.QuestionsStatistics.Add(new SaveQuestionStatisticsModel
        {
            QuestionId = question.Id,
            AnswersStatistics = question.Answers.Select(a => new SaveAnswerStatisticsModel { AnswerId = a.Id, AnsweredCount = 0 }).ToList()
        });
        game.Answers = 0;

        game.TimerCancellation = new CancellationTokenSource();
        var cancellationToken = game.TimerCancellation.Token;

        game.QuestionStartTime = DateTime.Now;

        var _ = Task.Run(async () =>
        {
            await Task.Delay(question.TimeLimit * 1000, cancellationToken);
            await EndRoundAsync(gameId, game, quiz);
        });
        return QuizQuestionMapper.MapToQuizQuestion(gameId, game.CurrentQuestion, question);
    }

    private class Player
    {
        public required string ConnectionId { get; set; }
        public required string Name { get; set; }
        public Guid? PlayerId { get; set; }
        public int Score { get; set; } = 0;
        public int LastAnsweredQuestion { get; set; } = -1;
    }
    private class RunningGame
    {
        public required int GameCode { get; set; }
        public required QuizDetailModel Quiz { get; set; }
        public required string HostID { get; set; }
        public required Guid GameID { get; set; }
        public List<Player> Players { get; } = [];
        public int CurrentQuestion { get; set; } = -1;
        public int Answers { get; set; } = 0;
        public CancellationTokenSource? TimerCancellation { get; set; } = null;
        public DateTime QuestionStartTime { get; set; } = DateTime.Now;
        public List<SaveQuestionStatisticsModel> QuestionsStatistics { get; set; } = [];

        public bool IsQuestionActive() { return (DateTime.Now - QuestionStartTime).TotalMilliseconds <= Quiz.Questions[CurrentQuestion].TimeLimit; }
    }

    private static int ComputeScore(DateTime startTime, DateTime answerTime, int timeLimit)
    {
        var dt = (answerTime - startTime).Nanoseconds;
        double timeLeftNs = timeLimit - dt;
        double limitNs = timeLimit * 1_000_000; // 1s = 1_000_000ns

        // from 1000 to 500 points for the correct answer
        return 500 + Math.Max(0, (int)Math.Ceiling(timeLeftNs / limitNs * 500));
    }
}
