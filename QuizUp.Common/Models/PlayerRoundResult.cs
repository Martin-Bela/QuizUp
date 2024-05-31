using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.Common.Models;

public enum AnswerResult { Correct, Incorrect, TimeExpired }
public class PlayerRoundResult
{
    public AnswerResult AnswerResult { get; set; }
    public int Score { get; set; }
}
