using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.Common.Models;
public class QuizQuestionAnswerModel
{
    public required QuizQuestionModel question;
    public required string answer;
}
