using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.MAUI.Services;
internal interface ISignalR
{
    event Action<string, string>? OnMessageReceived;
    Task StartAsync();
    Task StopAsync();
    Task SendMessage(string user, string message);
}
