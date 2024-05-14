namespace QuizUp.DAL.Entities;

public class Game : BaseEntity
{
    public Guid QuizId { get; set; }

    public Quiz Quiz { get; set; } = null!;

    public ICollection<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();

    public ICollection<GameApplicationUser> GameApplicationUsers { get; set; } = new List<GameApplicationUser>();

    public ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public ICollection<GameAnswer> GameAnswers { get; set; } = new List<GameAnswer>();
}
