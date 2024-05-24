namespace QuizUp.BL.Models;

public class CreateQuizModel
{
    public Guid UserId { get; set; }

    public required string Title { get; set; }

    public ICollection<CreateQuestionModel> Questions { get; set; } = new List<CreateQuestionModel>();
}
