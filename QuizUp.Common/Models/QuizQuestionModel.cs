namespace QuizUp.Common.Models;
public class QuizQuestionModel
{
    public required string GameId { get; set; }
    public required int QuestionId { get; set; }

    public int TimeLimit { get; set; }

    public required string Question { get; set; }
    public required string Answer1 { get; set; }
    public required string Answer2 { get; set; }
    public required string Answer3 { get; set; }
    public required string Answer4 { get; set; }
}
