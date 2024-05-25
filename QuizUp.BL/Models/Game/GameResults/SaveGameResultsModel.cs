namespace QuizUp.BL.Models;

public class SaveGameResultsModel
{
    public Guid GameId { get; set; }

    public IList<SavePlayerResultModel> PlayersResults { get; set; } = [];

    public IList<SaveQuestionStatisticsModel> QuestionsStatistics { get; set; } = [];
}
