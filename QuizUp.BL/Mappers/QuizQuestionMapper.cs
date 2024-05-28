using QuizUp.BL.Models;
using QuizUp.Common.Models;

namespace QuizUp.BL.Mappers;
internal class QuizQuestionMapper
{
    public static QuizQuestionModel MapToQuizQuestion(string gameID, int questionID, QuestionDetailModel quizQuestion)
    {
        return new QuizQuestionModel
        {
            GameId = gameID,
            QuestionId = questionID,
            TimeLimit = quizQuestion.TimeLimit,
            Question = quizQuestion.QuestionText,
            Answer1 = quizQuestion.Answers[0].AnswerText,
            Answer2 = quizQuestion.Answers[1].AnswerText,
            Answer3 = quizQuestion.Answers[2].AnswerText,
            Answer4 = quizQuestion.Answers[3].AnswerText,
        };
    }
}
