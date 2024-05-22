using QuizUp.Common.Models;

namespace QuizUp.Server.Services;

public interface IQuizService
{
    QuizQuestionModel getQuizQuestion(string quizId, int question);
}
