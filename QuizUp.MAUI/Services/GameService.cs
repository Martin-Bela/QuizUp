using System.Diagnostics;

namespace QuizUp.MAUI.Services;
public class GameService(IRoutingService routing) : IGameService
{
    public ISignalR? SignalR { get; private set; } = null;

    async public Task JoinGameAsync(int gameCode, string playerName)
    {
        SignalR = new SignalR(routing);
        await SignalR.StartAsync();
        await SignalR.JoinGameAsync(gameCode, playerName);
    }

    async public Task EndGameAsync()
    {
        if (SignalR != null)
        {
            await SignalR.StopAsync();
            SignalR = null;
        }
    }

    async public Task AnswerQuestionAsync(string gameID, int question, string answer)
    {
        Debug.Assert(SignalR != null);
        await SignalR.AnswerQuestionAsync(gameID, question, answer);
    }
}
