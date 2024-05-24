namespace QuizUp.BL.Models;

public class GameResultsModel
{
    public required string Title { get; set; }

    public List<PlayerResultModel> Leaderboard { get; set; } = new List<PlayerResultModel>();

    public List<QuestionStatisticsModel> QuestionsStatistics { get; set; } = new List<QuestionStatisticsModel>();
}
