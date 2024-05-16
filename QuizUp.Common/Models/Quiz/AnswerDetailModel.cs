namespace QuizUp.Common.Models;

public class AnswerDetailModel : ModelBase
{
    public Guid? Id { get; set; }

    public required string AnswerText { get; set; }

    public bool IsCorrect { get; set; } = false;

    public Guid? QuestionId { get; set; }
}
