﻿namespace QuizUp.BL.Models;

public class CreateAnswerModel
{
    public required string AnswerText { get; set; }

    public bool IsCorrect { get; set; } = false;
}
