namespace QuizUp.DAL.Entities;

public class GameAnswer
{
    public Guid GameId { get; set; }

    public Game Game { get; set; } = null!;

    public Guid AnswerId { get; set; }

    public Answer Answer { get; set; } = null!;

    public int AnsweredCount { get; set; }
}
