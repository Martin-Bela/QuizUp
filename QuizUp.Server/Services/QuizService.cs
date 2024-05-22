using QuizUp.BL.Services;
using QuizUp.Common.Models;

namespace QuizUp.Server.Services;

public class QuizService : IQuizService
{
    //Quiz[] quizes =
    //[
    //    new Quiz
    //    {
    //        questions =
    //        [
    //            new QuizQuestionAnswer { question = new QuizQuestion { GameId = "0", QuestionId = 0, Question = "What is 2 + 2?", Answer1 = "1", Answer2 = "2", Answer3 = "3", Answer4 = "4" }, answer = "4" },
    //            new QuizQuestionAnswer { question = new QuizQuestion { GameId = "0", QuestionId = 1, Question = "What is 2 + 3?", Answer1 = "1", Answer2 = "2", Answer3 = "3", Answer4 = "5" }, answer = "5" },
    //            new QuizQuestionAnswer { question = new QuizQuestion { GameId = "0", QuestionId = 2, Question = "What is 2 + 4?", Answer1 = "1", Answer2 = "2", Answer3 = "3", Answer4 = "6" }, answer = "6" }
    //        ]
    //    }
    //];

    public QuizQuestionModel getQuizQuestion(string quizId, int questionId)
    {
        //return quizes[int.Parse(quizId)].questions[questionId].question;
        return new QuizQuestionModel { GameId = "0", QuestionId = 0, Question = "What is 2 + 2?", Answer1 = "1", Answer2 = "2", Answer3 = "3", Answer4 = "4" };
    }
}
