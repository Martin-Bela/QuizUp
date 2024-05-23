﻿using QuizUp.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.MAUI.Services;
public interface IGameService
{
    string GameId { get; set; }
    bool IsHost { get; }
    Task CreateGame(Guid quizId);
    Task StartGameAsync();
    Task JoinGameAsync(int gameCode, string playerName);
    Task EndGameAsync();
    Task AnswerQuestionAsync(int question, int answer);
    Task NextQuestion();
}
