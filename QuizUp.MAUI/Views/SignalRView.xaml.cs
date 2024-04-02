using Microsoft.AspNetCore.SignalR.Client;
using QuizUp.MAUI.Services;

namespace QuizUp.MAUI.Views;

public partial class SignalRView : ContentPage
{
    readonly ISignalR signalR;

    public SignalRView()
    {
        InitializeComponent();
        lblChat.Text ??= string.Empty;
        signalR = new SignalR();
        _ = signalR.StartAsync();
        signalR.OnMessageReceived += (user, message) =>
        {
            Dispatcher.Dispatch(() =>
            {
                lblChat.Text += $"{user}: {message}\n";
            });
        };
    }

    private async void btnSend_Clicked(object sender, EventArgs e)
    {
        await signalR.SendMessage(txtUsername.Text, txtMessage.Text);

        txtMessage.Text = string.Empty;
    }
}