namespace QuizUp.BL.Models;

public class QuestionDetailModel : ModelBase
{
    public Guid Id { get; set; }

    public required string QuestionText { get; set; }

    public int TimeLimit { get; set; }

    public IList<AnswerDetailModel> Answers{ get; set; } = new List<AnswerDetailModel>();
}
