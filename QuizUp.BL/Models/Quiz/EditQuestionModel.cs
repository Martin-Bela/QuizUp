namespace QuizUp.BL.Models;

public class EditQuestionModel
{
    public Guid Id { get; set; }

    public required string QuestionText { get; set; }

    public int TimeLimit { get; set; }

    public ICollection<EditAnswerModel> Answers = new List<EditAnswerModel>();
}
