using QuizUp.Common.Models;

namespace QuizUp.BL.Services;

public interface IQuizService
{
    public Task<List<QuizSummaryModel>> GetQuizzessByUserIdAsync(Guid userId);

    public Task<QuizDetailModel> GetQuizByIdAsync(Guid quizId);

    public Task<QuizDetailModel> CreateQuizAsync(CreateQuizModel createQuizModel);

    public Task EditQuizAsync(EditQuizModel editQuizModel);

    public Task DeleteQuizByIdAsync(Guid quizId);
}
