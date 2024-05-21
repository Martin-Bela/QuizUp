using System.Diagnostics;

namespace QuizUp.MAUI.Services;
public class GameService(IRoutingService routing) : IGameService
{
    public ISignalR? SignalR { get; private set; } = null;
    public string GameId { get; private set; } = string.Empty;

    async public Task StartGameAsync(string gameId)
    {
        SignalR = new SignalR(routing);
        GameId = gameId;
        await SignalR.StartAsync();
        await SignalR.JoinGameAsync(gameId);
    }

    async public Task EndGameAsync()
    {
        if (SignalR != null)
        {
            await SignalR.StopAsync();
            SignalR = null;
        }
    }

    async public Task AnswerQuestionAsync(int question, string answer)
    {
        Debug.Assert(SignalR != null);
        await SignalR.AnswerQuestionAsync(GameId, question, answer);
    }
}
