using System.Diagnostics;

namespace QuizUp.MAUI.Services;
public class GameManager()
{
    public ISignalR? SignalR { get; set; } = null;
    public string GameId { get; set; } = string.Empty;

    async public Task StartGameAsync(string gameId)
    {
        SignalR = new SignalR();
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
