using QuizUp.BL.Models;

namespace QuizUp.BL.Services;

public interface IQuizService
{
    public Task<bool> DoesQuizBelongToUser(Guid quizId, Guid userId);

    public Task<List<QuizSummaryModel>> GetQuizzessByUserIdAsync(Guid userId);

    public Task<QuizDetailModel> GetQuizByIdAsync(Guid quizId);

    public Task<QuizDetailModel> CreateQuizAsync(CreateQuizModel createQuizModel);

    public Task EditQuizAsync(Guid quizId, EditQuizModel editQuizModel);

    public Task DeleteQuizByIdAsync(Guid quizId);

    public Task<QuizGamesModel> GetGamesByQuizIdAsync(Guid quiz);
}
