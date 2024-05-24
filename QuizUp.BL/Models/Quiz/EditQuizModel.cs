namespace QuizUp.BL.Models;

public class EditQuizModel
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public IList<EditQuestionModel> Questions { get; set; } = new List<EditQuestionModel>();
}
