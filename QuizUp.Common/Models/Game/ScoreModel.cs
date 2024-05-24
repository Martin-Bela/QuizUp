using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.Common.Models;
public class ScoreModel
{
    public required string PlayerNickname { get; set; }
    public required int Score { get; set; }
}
