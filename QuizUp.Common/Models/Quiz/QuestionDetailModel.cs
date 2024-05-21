namespace QuizUp.Common.Models;

public class QuestionDetailModel : ModelBase
{
    public Guid Id { get; set; }

    public required string QuestionText { get; set; }

    public int TimeLimit { get; set; }

    public List<AnswerDetailModel> Answers = new List<AnswerDetailModel>();
}
