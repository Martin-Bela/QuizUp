namespace QuizUp.BL.Models;

public class EditQuizModel
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public ICollection<EditQuestionModel> Questions { get; set; } = new List<EditQuestionModel>();
}
