namespace QuizUp.Common.Models;

public class AnswerStatisticsModel
{
    public required string AnswerText { get; set; }

    public int AnsweredCount { get; set; }
}
