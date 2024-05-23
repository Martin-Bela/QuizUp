﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.Services;

namespace QuizUp.MAUI.ViewModels;

public partial class JoinGameViewModel(ViewModelBase.Dependencies dependencies, IGameService gameManager) : ViewModelBase(dependencies)
{
    [ObservableProperty]
    public string? gameId;

    [ObservableProperty]
    public string? nickName;

    [RelayCommand]
    void JoinGame()
    {
        if (string.IsNullOrWhiteSpace(GameId))
        {
            return;
        }

        if (!int.TryParse(GameId, out var gameCode))
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(NickName))
        {
            NickName = "Player";
        }

        Task.Run(
            async () =>
            {
                await gameManager.JoinGameAsync(gameCode, NickName);
            }
        );
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        var route = routingService.GetRouteByViewModel<QuizListViewModel>();
        await Shell.Current.GoToAsync(route);
    }
}
