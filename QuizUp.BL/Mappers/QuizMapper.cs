using QuizUp.DAL.Entities;
using QuizUp.Common.Models;
using Riok.Mapperly.Abstractions;

namespace QuizUp.BL.Mappers;

[Mapper]
public static partial class QuizMapper
{
    public static partial QuizSummaryModel MapToQuizSummaryModel(this Quiz quiz);

    public static partial QuizDetailModel MapToQuizDetailModel(this Quiz quiz);

    [MapProperty(nameof(CreateQuizModel.UserId), nameof(Quiz.ApplicationUserId))]
    public static partial Quiz MapToQuiz(this CreateQuizModel quizDetail);

    public static partial Question MapToQuestion(this EditQuestionModel question);

    public static partial Answer MapToAnswer(this EditAnswerModel editAnswerModel);
}
