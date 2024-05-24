namespace QuizUp.Common.Models;

public class SaveQuestionStatisticsModel
{
    public Guid QuestionId { get; set; }

    public IList<SaveAnswerStatisticsModel> AnswersStatistics { get; set; } = new List<SaveAnswerStatisticsModel>();
}
