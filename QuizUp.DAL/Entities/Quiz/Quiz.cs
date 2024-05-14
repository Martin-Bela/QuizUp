namespace QuizUp.DAL.Entities;

public class Quiz : BaseEntity
{
    public required string Title { get; set; }

    public Guid ApplicationUserId { get; set; }

    public ApplicationUser ApplicationUser { get; set; } = null!;

    public ICollection<Question> Questions { get; set; } = new List<Question>();

    public ICollection<Game> Games { get; set; } = new List<Game>();
}
