using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Services;
using QuizUp.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.MAUI.ViewModels;

[QueryProperty(nameof(GameId), nameof(GameId))]

public partial class GameIntroViewModel(ViewModelBase.Dependencies dependencies, IGameService gameService) : ViewModelBase(dependencies)
{
    private string? gameId;
    public string? GameId
    {
        get => gameId;
        set
        {
            if (value == null)
            {
                return;
            }
            gameId = value;
            gameService.GameId = value;
        }
    }
}
