namespace QuizUp.Common.Models;

public class SaveGameResultsModel
{
    public Guid GameId { get; set; }

    public List<SavePlayerResultModel> PlayersResults { get; set; } = new List<SavePlayerResultModel>();

    public List<SaveQuestionStatisticsModel> QuestionsStatistics { get; set; } = new List<SaveQuestionStatisticsModel>();
}
