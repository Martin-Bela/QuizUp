namespace QuizUp.BL.Models;

public class QuizDetailModel : ModelBase
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public IList<QuestionDetailModel> Questions { get; set; } = new List<QuestionDetailModel>();
}
