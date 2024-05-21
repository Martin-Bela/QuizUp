using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.Common.Dtos;
public class QuizQuestionAnswer
{
    public required QuizQuestion question;
    public required string answer;
}
