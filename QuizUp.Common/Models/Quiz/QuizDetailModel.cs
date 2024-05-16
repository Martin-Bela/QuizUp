namespace QuizUp.Common.Models;

public class QuizDetailModel : ModelBase
{
    public Guid? Id { get; set; }

    public required string Title { get; set; }

    public ICollection<QuestionDetailModel> Questions { get; set; } = new List<QuestionDetailModel>();
}
