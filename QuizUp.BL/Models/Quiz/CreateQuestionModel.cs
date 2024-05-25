namespace QuizUp.BL.Models;

public class CreateQuestionModel
{
    public required string QuestionText { get; set; }

    public int TimeLimit { get; set; }

    public IList<CreateAnswerModel> Answers { get; set; } = [];
}
