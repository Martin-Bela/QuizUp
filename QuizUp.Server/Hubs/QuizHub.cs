using Microsoft.AspNetCore.SignalR;
using QuizUp.Server.Services;
//using QuizUp.BL.Services;
using System.Diagnostics;

namespace QuizUp.Server.Hubs;

public class QuizHub(IQuizService quizService) : Hub
{
    public Task StartQuiz(string gameId)
    {
        //await Clients.Group(gameId).SendAsync("QuizStarted", quizService.getQuizQuestion(gameId, 0));
        throw new NotImplementedException();
    }

    public async Task JoinQuiz(string gameId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, gameId);

        var player = Context.ConnectionId;
        Debug.WriteLine($"Player {player} joined game {gameId}");
        await Clients.Client(player).SendAsync("NextQuestion", quizService.getQuizQuestion("0", 0));
    }

    public async Task LeaveQuiz(string gameId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
    }

    public async Task Answer(string gameId, int question, string answer)
    {
        var player = Context.ConnectionId;

        //todo check answer
        Debug.WriteLine($"Player {player} answered {answer}");
        await Task.Delay(2000);
        await Clients.Client(player).SendAsync("NextQuestion", quizService.getQuizQuestion("0", question + 1));
    }
}
