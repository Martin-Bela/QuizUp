namespace QuizUp.BL.Models;

public class QuizSummaryModel : ModelBase
{
    public Guid Id { get; set; }

    public required string Title { get; set; }
}
