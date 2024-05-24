namespace QuizUp.BL.Models;

public class SaveAnswerStatisticsModel
{
    public Guid AnswerId { get; set; }

    public int AnsweredCount { get; set; }
}
