using Microsoft.AspNetCore.SignalR.Client;

namespace QuizUp.MAUI.Views;

public partial class SignalR : ContentPage
{
    readonly HubConnection hubConnection;
    public SignalR()
    {
        InitializeComponent();

        var baseUrl = "https://localhost";

        hubConnection = new HubConnectionBuilder()
            .WithUrl($"{baseUrl}:7126/quizHub")
            .Build()
            ?? throw new Exception("Unable to connect to SignalR server!");

        lblChat.Text ??= string.Empty;

        hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            Dispatcher.Dispatch(() =>
            {
                lblChat.Text += $"<b>{user}</b>: {message}<br/>";
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

        txtMessage.Text = string.Empty;
    }
}