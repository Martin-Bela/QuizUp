namespace QuizUp.Common.Models;

public class GameSummaryModel
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
