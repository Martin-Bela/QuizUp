using Microsoft.AspNetCore.SignalR;
using QuizUp.BL.Services;
using System.Diagnostics;

namespace QuizUp.Server.Hubs;

using QuizUp.Common;

public class QuizHub : Hub
{
    IGameManager gameManager;
    public QuizHub(IGameManager gameManager)
    {
        this.gameManager = gameManager;
        gameManager.OnRoundEnded += async (gameId, quizOver, bestPlayers) =>
        {
            await Clients.Group(gameId).SendAsync(SignalRHubCommands.Score, quizOver, bestPlayers);
            await Clients.Client(gameManager.GetHostID(gameId)).SendAsync(SignalRHubCommands.Score, quizOver, bestPlayers);
        };
    }

    public async Task CreateGame(Guid quizId)
    {
        var (passCode, gameId, quizName) = await gameManager.CreateGameAsync(quizId, Context.ConnectionId);
        await Clients.Caller.SendAsync(SignalRHubCommands.GameCreated, passCode, gameId, quizName);
    }
    public async Task JoinGame(int gameCode, string playerName, Guid? playerId)
    {
        var player = Context.ConnectionId;
        string gameId = await gameManager.AddPlayerAsync(gameCode, player, playerName, playerId);

        await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
        Debug.WriteLine($"Player {playerName}({player}) joined game {gameCode}");
        await Clients.Caller.SendAsync(SignalRHubCommands.GameJoined, gameId);
    }
    public async Task NextQuestion(string gameId)
    {
        var question = await gameManager.NextQuestionAsync(gameId, Context.ConnectionId);
        await Clients.Group(gameId).SendAsync(SignalRHubCommands.NextQuestion, question);
        await Clients.Caller.SendAsync(SignalRHubCommands.NextQuestion, question);
    }

    public async Task Answer(string gameId, int question, int answer)
    {
        var allAnswered = await gameManager.AnswerAsync(gameId, question, answer, Context.ConnectionId);
        if (!allAnswered)
        {
            await Clients.Caller.SendAsync(SignalRHubCommands.AnswerAccepted);
        }
    }

    public async Task LeaveQuiz(string gameId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
    }
}
