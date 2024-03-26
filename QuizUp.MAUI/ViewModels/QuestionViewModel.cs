using CommunityToolkit.Mvvm.Input;
using QuizUp.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.MAUI.ViewModels;
public partial class QuestionViewModel : IViewModel
{
    public QuizQuestion QuizQuestion { get; set; } = new("Q", ["A", "B", "C", "D"]);

    [RelayCommand]
    void Answer(string number)
    {
        Console.WriteLine("");
    }
}
