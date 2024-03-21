using Microsoft.AspNetCore.SignalR;

namespace QuizUp.Server.Hubs;

public class GameHub : Hub
{
    public async Task SendMessageToAll(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
