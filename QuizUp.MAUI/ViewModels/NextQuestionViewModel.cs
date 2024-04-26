using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.MAUI.ViewModels;

public partial class NextQuestionViewModel() : ViewModelBase
{
    [RelayCommand]
    void Answer(string number)
    {
        Console.WriteLine("");
    }
}
