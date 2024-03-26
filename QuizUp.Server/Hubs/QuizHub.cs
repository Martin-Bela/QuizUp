using Microsoft.AspNetCore.SignalR;

namespace QuizUp.Server.Hubs;

public class QuizHub : Hub
{
    public async Task StartQuiz(string gameId)
    {

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
