﻿using QuizUp.BL.Models;

namespace QuizUp.BL.Services;

public interface IQuizService
{
    //todo: Remove this method
    public Task<Guid> GetFirstQuizID();

    public Task<List<QuizSummaryModel>> GetQuizzessByUserIdAsync(Guid userId);

    public Task<QuizDetailModel> GetQuizByIdAsync(Guid quizId);

    public Task<QuizDetailModel> CreateQuizAsync(CreateQuizModel createQuizModel);

    public Task EditQuizAsync(Guid quizId, EditQuizModel editQuizModel);

    public Task DeleteQuizByIdAsync(Guid quizId);

    public Task<QuizResultsModel> GetQuizResultsByIdAsync(Guid quiz);
}
