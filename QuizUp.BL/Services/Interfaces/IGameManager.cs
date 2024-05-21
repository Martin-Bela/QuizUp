using QuizUp.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.BL.Services;
public interface IGameManager
{
    Task StartGame(Guid quizId, string hostId);
    Task AddPlayer(string gameId, string player, string playerName);
    Task<bool> Answer(string gameId, int question, string answer, string connectionId);
    Task<QuizQuestion?> NextQuestion(string gameId, string connectionId);
}
