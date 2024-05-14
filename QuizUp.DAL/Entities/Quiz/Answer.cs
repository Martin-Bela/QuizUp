namespace QuizUp.DAL.Entities;

public class Answer : BaseEntity
{
    public required string AnswerText { get; set; }

    public Boolean IsCorrect { get; set; } = false;

    public Guid QuestionId { get; set; }

    public Question Question { get; set; } = null!;

    public ICollection<Game> Games { get; set; } = new List<Game>();

    public ICollection<GameAnswer> GameAnswers { get; set; } = new List<GameAnswer>();
}
