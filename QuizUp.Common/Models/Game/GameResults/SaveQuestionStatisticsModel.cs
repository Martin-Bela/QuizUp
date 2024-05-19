namespace QuizUp.Common.Models;

public class SaveQuestionStatisticsModel
{
    public Guid QuestionId { get; set; }

    public ICollection<SaveAnswerStatisticsModel> AnswersStatistics { get; set; } = new List<SaveAnswerStatisticsModel>();
}
