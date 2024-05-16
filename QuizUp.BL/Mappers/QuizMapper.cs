using QuizUp.DAL.Entities;
using QuizUp.Common.Models;
using Riok.Mapperly.Abstractions;

namespace QuizUp.BL.Mappers;

[Mapper]
public static partial class QuizMapper
{
    public static partial Quiz MapToQuiz(this QuizDetailModel quizDetail);

    public static partial QuizDetailModel MapToQuizDetailModel(this Quiz quiz);

    public static partial QuizSummaryModel MapToQuizSummaryModel(this Quiz quiz);

    public static partial Question MapToQuestion(this QuestionDetailModel questionDetailModel);

    public static partial QuestionDetailModel MapToQuestionDetailModel(this Question question);

    public static partial Answer MapToAnswer(this AnswerDetailModel answerDetailModel);

    public static partial AnswerDetailModel MapToAnswerDetailModel(this Answer answer);
}
