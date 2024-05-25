namespace QuizUp.BL.Models;

public class GameResultsModel
{
    public required string Title { get; set; }

    public List<PlayerResultModel> Leaderboard { get; set; } = [];

    public List<QuestionStatisticsModel> QuestionsStatistics { get; set; } = [];
}
