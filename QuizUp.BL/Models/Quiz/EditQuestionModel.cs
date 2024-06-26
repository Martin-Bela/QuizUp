﻿namespace QuizUp.BL.Models;

public class EditQuestionModel
{
    public Guid Id { get; set; }

    public required string QuestionText { get; set; }

    public int TimeLimit { get; set; }

    public IList<EditAnswerModel> Answers { get; set; } = new List<EditAnswerModel>();
}
