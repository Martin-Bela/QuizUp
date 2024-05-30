using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.BL.Models.Game;
public class GameStartDataModel
{
    public int PassCode { get; set; }
    public Guid GameId { get; set; }
    public required string QuizName { get; set; }
    public required List<string> Players { get; set; }
}
