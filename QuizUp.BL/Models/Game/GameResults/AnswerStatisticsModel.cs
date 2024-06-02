namespace QuizUp.BL.Models;

public class AnswerStatisticsModel
{
    public required string AnswerText { get; set; }

    public int AnsweredCount { get; set; }

    public bool IsCorrect { get; set; }
}
