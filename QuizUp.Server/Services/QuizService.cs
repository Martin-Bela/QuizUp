using QuizUp.Common.Models;

namespace QuizUp.Server.Services;

public class QuizService : IQuizService
{
    Quiz[] quizes =
    [
        new Quiz
        {
            questions =
            [
                new QuizQuestionAnswer{ question = new QuizQuestion("What is 1+1?", ["1", "2", "3", "4"]), answer = "1" },
                new QuizQuestionAnswer{ question = new QuizQuestion("What is 2+2?", ["1", "2", "3", "4"]), answer = "3" },
                new QuizQuestionAnswer{ question = new QuizQuestion("What is 3+3?", ["1", "2", "4", "6"]), answer = "3" },
            ]
        }
    ];

    public QuizQuestion getQuizQuestion(string quizId, int questionId)
    {
        return quizes[int.Parse(quizId)].questions[questionId].question;
    }
}
