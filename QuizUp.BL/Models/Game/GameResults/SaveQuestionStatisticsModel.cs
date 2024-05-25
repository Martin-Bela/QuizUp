namespace QuizUp.BL.Models;

public class SaveQuestionStatisticsModel
{
    public Guid QuestionId { get; set; }

    public IList<SaveAnswerStatisticsModel> AnswersStatistics { get; set; } = [];
}
