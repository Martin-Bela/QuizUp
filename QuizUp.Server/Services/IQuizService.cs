using QuizUp.Common.Models;

namespace QuizUp.Server.Services;

public interface IQuizService
{
    QuizQuestion getQuizQuestion(string quizId, int question);
}
