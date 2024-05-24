namespace QuizUp.BL.Models;

public class CreateGameResultModel
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public int Code { get; set; }
}
