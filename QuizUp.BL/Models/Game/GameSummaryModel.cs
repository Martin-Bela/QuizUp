namespace QuizUp.BL.Models;

public class GameSummaryModel
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
