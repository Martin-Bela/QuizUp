using Riok.Mapperly.Abstractions;
using QuizUp.MAUI.Api;

namespace QuizUp.MAUI.Mappers
{
    [Mapper]
    public static partial class QuizMapper
    {
        public static partial CreateQuestionModel MapToCreateQuestionModel(this QuestionDetailModel quiz);

        public static partial EditQuizModel MapToEditQuizModel(this QuizDetailModel quiz);
    }
}
