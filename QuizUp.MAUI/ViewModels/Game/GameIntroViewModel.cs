﻿using QuizUp.MAUI.Services;

namespace QuizUp.MAUI.ViewModels;

[QueryProperty(nameof(GameId), nameof(GameId))]

public partial class GameIntroViewModel(ViewModelBase.Dependencies dependencies, IRunningGameService gameService) : ViewModelBase(dependencies)
{
    private Guid gameId;
    public Guid GameId
    {
        get => gameId;
        set
        {
            gameId = value;
            gameService.GameId = value;
        }
    }
}
