using Autofac;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizUp.Common.Models;
using QuizUp.MAUI.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.MAUI.ViewModels;

[QueryProperty(nameof(QuizQuestion), nameof(QuizQuestion))]
public partial class QuestionViewModel(GameManager gameManager) : ViewModelBase
{
    [ObservableProperty]
    public QuizQuestion quizQuestion = new() { GameId = "-1", QuestionId = 0, Answer1 = "1", Answer2 = "2", Answer3 = "3", Answer4 = "4", Question = "QuestionPlaceholder" };

    [RelayCommand]
    async Task Answer(string number)
    {
        await gameManager.AnswerQuestionAsync(quizQuestion.QuestionId, number);
    }
}
