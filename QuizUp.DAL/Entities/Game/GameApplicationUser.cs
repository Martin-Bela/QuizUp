namespace QuizUp.DAL.Entities;

public class GameApplicationUser
{
    public Guid GameId { get; set; }

    public Game Game { get; set; } = null!;

    public Guid ApplicationUserId { get; set; }

    public ApplicationUser ApplicationUser { get; set; } = null!;

    public int Score { get; set; }
}
