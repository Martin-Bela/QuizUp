using Microsoft.AspNetCore.SignalR;
using QuizUp.Server.Services;

namespace QuizUp.Server.Hubs;

public class QuizHub(IQuizService quizService) : Hub
{
    public async Task StartQuiz(string gameId)
    {
        await Clients.Group(gameId).SendAsync("QuizStarted", quizService.getQuizQuestion(gameId, 0));
    }

    public async Task JoinQuiz(string gameId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
    }

    public async Task LeaveQuiz(string gameId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
    }

    public async Task Answer(string gameId, int question, string answer)
    {
        var player = Context.ConnectionId;
    }

    //todo remove
    public async Task SendMessageToAll(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
