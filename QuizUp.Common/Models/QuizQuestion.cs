using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.Common.Models;
public class QuizQuestion
{
    public string Question { get; set; }
    public string Answer1 { get; set; }
    public string Answer2 { get; set; }
    public string Answer3 { get; set; }
    public string Answer4 { get; set; }


    public QuizQuestion(string question, string[] answers)
    {
        if (answers.Length != 4)
        {
            throw new ArgumentException("Only 4 answers supported!");
        }
        Question = question;
        Answer1 = answers[0];
        Answer2 = answers[1];
        Answer3 = answers[2];
        Answer4 = answers[3];
    }
}
