using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.MAUI.Services;
internal class SignalR : ISignalR
{
    readonly HubConnection hubConnection;
    public event Action<string, string>? OnMessageReceived;


    public SignalR()
    {
        var baseUrl = "https://localhost";
        hubConnection = new HubConnectionBuilder()
                .WithUrl($"{baseUrl}:7126/quizHub")
                .Build()
                ?? throw new Exception("Unable to connect to SignalR server!");

        hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                if (OnMessageReceived is not null)
                {
                    OnMessageReceived(user, message);
                }
            });
    }

    public async Task StartAsync()
    {
        await hubConnection.StartAsync();
    }

    public async Task StopAsync()
    {
        await hubConnection.StopAsync();
    }

    public async Task SendMessage(string user, string message)
    {
        await hubConnection.InvokeAsync("SendMessageToAll", user, message);
    }
}
