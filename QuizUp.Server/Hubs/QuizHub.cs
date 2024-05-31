using Microsoft.AspNetCore.SignalR;
using QuizUp.BL.Services;
using System.Diagnostics;

namespace QuizUp.Server.Hubs;

using QuizUp.Common;
using QuizUp.Common.Models;
using QuizUp.DAL.Entities;

public class QuizHub : Hub
{
    IGameManager gameManager;
    public QuizHub(IGameManager gameManager)
    {
        this.gameManager = gameManager;
        gameManager.OnRoundEnded += async (gameId, quizOver, bestPlayers, hostId, playerResults) =>
        {
            foreach (var (playerConnectionId, playerResult) in playerResults)
            {
                await Clients.Client(playerConnectionId).SendAsync(SignalRHubCommands.Score, quizOver, bestPlayers, playerResult);
            }
            await Clients.Client(hostId).SendAsync(SignalRHubCommands.Score, quizOver, bestPlayers);
        };
    }

    public async Task CreateGame(Guid quizId)
    {
        Guid gameId;
        try
        {
            gameId = await gameManager.CreateGameAsync(quizId, Context.ConnectionId);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Clients.Caller.SendAsync(SignalRHubCommands.GameError, ex.Message);
            return;
        }
        var gameStartData = gameManager.GetGameStartData(gameId);
        await Clients.Caller.SendAsync(SignalRHubCommands.GameCreated, gameStartData);
    }
    public async Task JoinGame(int gameCode, string playerName, Guid? playerId)
    {
        var player = Context.ConnectionId;
        Guid gameId = gameManager.AddPlayer(gameCode, player, playerName, playerId);

        await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
        Debug.WriteLine($"Player {playerName}({player}) joined game {gameCode}");
        await Clients.Caller.SendAsync(SignalRHubCommands.GameJoined, gameId);

        var gameStartData = gameManager.GetGameStartData(gameId);
        await Clients.Client(gameManager.GetHostID(gameId)).SendAsync(SignalRHubCommands.GameCreated, gameStartData);
    }
    public async Task NextQuestion(Guid gameId)
    {
        var question = gameManager.NextQuestion(gameId, Context.ConnectionId);
        await Clients.Group(gameId.ToString()).SendAsync(SignalRHubCommands.NextQuestion, question);
        await Clients.Caller.SendAsync(SignalRHubCommands.NextQuestion, question);
    }

    public async Task Answer(Guid gameId, int question, int answer)
    {
        var allAnswered = gameManager.Answer(gameId, question, answer, Context.ConnectionId);
        if (!allAnswered)
        {
            await Clients.Caller.SendAsync(SignalRHubCommands.AnswerAccepted);
        }
    }

    public async Task LeaveQuiz(Guid gameId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId.ToString());
    }
}
