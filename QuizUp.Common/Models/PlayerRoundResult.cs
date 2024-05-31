namespace QuizUp.Common.Models;

public enum AnswerResult { 
    Correct,
    Incorrect, 
    TimeExpired 
}

public class PlayerRoundResult
{
    public AnswerResult AnswerResult { get; set; }
    public int Score { get; set; }
}
