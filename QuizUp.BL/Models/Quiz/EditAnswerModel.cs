namespace QuizUp.BL.Models;

public class EditAnswerModel
{
    public Guid Id { get; set; }

    public required string AnswerText { get; set; }

    public bool IsCorrect { get; set; } = false;
}
