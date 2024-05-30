namespace QuizUp.BL.Models;

public class QuizGamesModel : ModelBase
{
    public Guid QuizId { get; set; }

    public required string Title { get; set; }

    public required IList<GameSummaryModel> Games { get; set; }
}
