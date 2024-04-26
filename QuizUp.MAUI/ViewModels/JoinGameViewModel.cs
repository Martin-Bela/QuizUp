using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.MAUI.ViewModels;
public partial class JoinGameViewModel() : ViewModelBase
{
    [ObservableProperty]
    public string? gameId;

    [RelayCommand]
    void JoinGame()
    {
        Debug.WriteLine("Joining game");
    }
}
