using QuizUp.Common.Dtos;

namespace QuizUp.Server.Services;

public interface IQuizService
{
    QuizQuestion getQuizQuestion(string quizId, int question);
}
