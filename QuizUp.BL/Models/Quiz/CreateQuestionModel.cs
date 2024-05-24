namespace QuizUp.BL.Models;

public class CreateQuestionModel
{
    public required string QuestionText { get; set; }

    public int TimeLimit { get; set; }

    public ICollection<CreateAnswerModel> Answers = new List<CreateAnswerModel>();

}
