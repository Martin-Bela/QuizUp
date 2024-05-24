using System.Diagnostics;

namespace QuizUp.MAUI.Services;
public class GameService(IRoutingService routing) : IGameService
{
    public ISignalR? SignalR { get; private set; } = null;
    public string GameId { get; set; } = string.Empty;
    public bool IsHost { get; set; }

    async public Task JoinGameAsync(int gameCode, string playerName, Guid? playerId)
    {
        SignalR = new SignalR(routing);
        IsHost = false;
        await SignalR.StartAsync();
        await SignalR.JoinGameAsync(gameCode, playerName, playerId);
    }

    async public Task EndGameAsync()
    {
        if (SignalR != null)
        {
            await SignalR.StopAsync();
            SignalR = null;
        }
    }

    async public Task AnswerQuestionAsync(int question, int answer)
    {
        Debug.Assert(SignalR != null);
        await SignalR.AnswerQuestionAsync(GameId, question, answer);
    }

    async public Task CreateGame(Guid quizId)
    {
        SignalR = new SignalR(routing);
        IsHost = true;
        await SignalR.StartAsync();
        await SignalR.CreateGameAsync(quizId);
    }

    async public Task StartGameAsync()
    {
        Debug.Assert(SignalR != null);
        Debug.Assert(GameId != null);
        await SignalR.StartGameAsync(GameId);
    }

    async public Task NextQuestion()
    {
        Debug.Assert(SignalR != null);
        Debug.Assert(GameId != null);
        await SignalR.NextQuestionAsync(GameId);
    }
}
