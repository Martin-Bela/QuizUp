namespace QuizUp.BL.Models;

public class AnswerDetailModel : ModelBase
{
    public Guid Id { get; set; }

    public required string AnswerText { get; set; }

    public bool IsCorrect { get; set; } = false;
}
