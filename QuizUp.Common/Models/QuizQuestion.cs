using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.Common.Models;
public class QuizQuestion
{
    public required string GameId { get; set; }
    public required int QuestionId { get; set; }

    public required string Question { get; set; }
    public required string Answer1 { get; set; }
    public required string Answer2 { get; set; }
    public required string Answer3 { get; set; }
    public required string Answer4 { get; set; }
}
