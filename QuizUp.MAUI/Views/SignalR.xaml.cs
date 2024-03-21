using Microsoft.AspNetCore.SignalR.Client;

namespace QuizUp.MAUI.Views;

public partial class SignalR : ContentPage
{
    HubConnection hubConnection;
    public SignalR()
    {
        InitializeComponent();

        var baseUrl = "https://localhost";

        var connection = new HubConnectionBuilder()
            .WithUrl($"{baseUrl}:7126/chatHub")
            .Build();

        if (connection == null)
        {
            throw new Exception("Unable to connect to SignalR server!");
        }
        hubConnection = connection;

        lblChat.Text ??= "";

        hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            Dispatcher.Dispatch(() =>
            {
                lblChat.Text = $"<b>{user}</b>: {message}<br/>";
            });
        });

        Task.Run(() =>
        {
            Dispatcher.Dispatch(async () =>
            {
                await hubConnection.StartAsync();
            });
        });
    }

    private async void btnSend_Clicked(object sender, EventArgs e)
    {
        await hubConnection.InvokeCoreAsync("SendMessageToAll", args: new[]
        {
            txtUsername.Text,
            txtMessage.Text
        });

        txtMessage.Text = String.Empty;
    }
}