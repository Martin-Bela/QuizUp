using QuizUp.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.BL.Services;
public interface IGameManager
{
    Task StartGameAsync(Guid quizId, string hostId);
    Task<string> AddPlayerAsync(int gameCode, string playerID, string playerName);
    Task<bool> AnswerAsync(string gameId, int question, string answer, string connectionId);
    Task<QuizQuestionModel?> NextQuestionAsync(string gameId, string connectionId);
}
