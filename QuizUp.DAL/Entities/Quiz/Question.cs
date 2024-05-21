namespace QuizUp.DAL.Entities;

public class Question : BaseEntity
{
    public required string QuestionText { get; set; }

    public int TimeLimit { get; set; }

    public Guid QuizId { get; set; }

    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
}
