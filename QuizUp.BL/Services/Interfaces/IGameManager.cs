using QuizUp.BL.Models;
using QuizUp.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.BL.Services;
public interface IGameManager
{
    string GetHostID(string gameId);
    Func<string, bool, List<ScoreModel>, Task>? OnRoundEnded { get; set; }

    Task<(int passCode, string gameId, string quizName)> CreateGameAsync(Guid quizId, string hostId);
    Task<string> AddPlayerAsync(int gameCode, string playerID, string playerName, Guid? PlayerId);
    Task<bool> AnswerAsync(string gameId, int question, int answer, string connectionId);
    Task<QuizQuestionModel?> NextQuestionAsync(string gameId, string connectionId);
}
