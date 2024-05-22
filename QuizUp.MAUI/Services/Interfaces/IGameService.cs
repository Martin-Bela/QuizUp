using QuizUp.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.MAUI.Services;
internal interface IGameService
{
    Task JoinGameAsync(int gameCode, string playerName);
    Task EndGameAsync();
    Task AnswerQuestionAsync(string gameId, int question, string answer);
    Task StartGameAsync(string gameId);
}
