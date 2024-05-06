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
public partial class QuestionViewModel(GameManager gameManager, IDispatcher dispatcher) : ViewModelBase
{
    QuizQuestion quizQuestion = new() { GameId = "-1", QuestionId = 0, Answer1 = "1", Answer2 = "2", Answer3 = "3", Answer4 = "4", Question = "QuestionPlaceholder" };

    public QuizQuestion QuizQuestion
    {
        get => quizQuestion;
        set
        {
            SetProperty(ref quizQuestion, value);
            SelectedAnswer = -1;
            RemainingTime = "30s";
            CreateTimer();
        }
    }

    [ObservableProperty]
    public string remainingTime = "30s";

    [ObservableProperty]
    public int selectedAnswer = -1;

    IDispatcherTimer? timer = null;

    [RelayCommand]
    async Task Answer(string number)
    {
        if (SelectedAnswer != -1)
        {
            return;
        }
        SelectedAnswer = int.Parse(number);
        gameManager.AnswerQuestionAsync(quizQuestion.QuestionId, number);
    }

    public void CreateTimer()
    {
        timer?.Stop();
        timer = dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += (s, e) =>
        {
            dispatcher.Dispatch(() => RemainingTime = (int.Parse(RemainingTime.TrimEnd('s')) - 1).ToString() + "s");
        };
        timer.Start();
    }
}
