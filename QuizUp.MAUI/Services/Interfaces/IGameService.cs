using QuizUp.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.MAUI.Services;
internal interface IGameService
{
    public string GameId { get; }
    Task StartGameAsync(string gameId);
    Task EndGameAsync();
    Task AnswerQuestionAsync(int question, string answer);
}
