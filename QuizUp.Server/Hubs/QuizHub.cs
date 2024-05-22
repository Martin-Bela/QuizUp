using Microsoft.AspNetCore.SignalR;
using QuizUp.BL.Services;
using QuizUp.Server.Services;
//using QuizUp.BL.Services;
using System.Diagnostics;

namespace QuizUp.Server.Hubs;

public class QuizHub(IGameManager gameManager) : Hub
{
    public async Task JoinQuiz(int gameCode, string playerName)
    {
        var player = Context.ConnectionId;
        string gameId = await gameManager.AddPlayer(gameCode, player, playerName);

        await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
        Debug.WriteLine($"Player {playerName}({player}) joined game {gameCode}");
        await Clients.Client(player).SendAsync("GameJoined");
    }
    public async Task NextQuestion(string gameId)
    {
        var question = gameManager.NextQuestion(gameId, Context.ConnectionId);
        await Clients.Group(gameId).SendAsync("NextQuestion", question);
    }

    public async Task Answer(string gameId, int question, string answer)
    {
        var player = Context.ConnectionId;
        var allAnswered = await gameManager.Answer(gameId, question, answer, Context.ConnectionId);
        if (allAnswered)
        {
            await Clients.Group(gameId).SendAsync("Score");
        }
        await Clients.User(player).SendAsync("AnserAccepted");
    }

    public async Task LeaveQuiz(string gameId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
    }
}
