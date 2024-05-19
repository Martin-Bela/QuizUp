namespace QuizUp.Common.Models;

public class GameCreateResultModel
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public int Code { get; set; }
}
