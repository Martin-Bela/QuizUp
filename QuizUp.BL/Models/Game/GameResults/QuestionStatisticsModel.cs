namespace QuizUp.BL.Models;

public class QuestionStatisticsModel
{
    public required string QuestionText { get; set; }

    public ICollection<AnswerStatisticsModel> AnswersStatistics { get; set; } = new List<AnswerStatisticsModel>();
}
