namespace QuizUp.BL.Models;

public class QuizResultsModel
{
    public Guid QuizId { get; set; }

    public string QuizName { get; set; }

    public List<QuestionStatisticsModel> QuestionResults { get; set; }

    public List<PlayerResultModel> Scores { get; set; }
}