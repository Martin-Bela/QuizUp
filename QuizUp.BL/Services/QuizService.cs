using Microsoft.EntityFrameworkCore;
using QuizUp.BL.Mappers;
using QuizUp.Common.Models;
using QuizUp.DAL.Data;
using QuizUp.DAL.Entities;
using QuizUp.BL.Exceptions;

namespace QuizUp.BL.Services;

public class QuizService(ApplicationDbContext dbContext) : IQuizService
{
    public async Task<List<QuizSummaryModel>> GetQuizzessByUserIdAsync(Guid userId)
    {
        var user = await dbContext.ApplicationUsers.FindAsync(userId);
        if (user == null)
        {
            throw new NotFoundException($"Application user with id {userId} not found.");
        }

        var quizzes = await dbContext.Quizzes
            .Where(q => q.ApplicationUserId == userId)
            .Select(q => q.MapToQuizSummaryModel())
            .ToListAsync();

        return quizzes;
    }

    public async Task<QuizDetailModel> GetQuizByIdAsync(Guid quizId)
    {
        var quiz = await dbContext.Quizzes
            .Where(q => q.Id == quizId)
            .Include(q => q.Questions)
            .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync();

        if (quiz == null)
        {
            throw new NotFoundException($"Quiz with id ${quizId} not found.");
        }

        return quiz.MapToQuizDetailModel();
    }

    public async Task<QuizDetailModel> CreateQuizAsync(CreateQuizModel createQuizModel)
    {
        var newQuiz = createQuizModel.MapToQuiz();

        await dbContext.Quizzes.AddAsync(newQuiz);

        await dbContext.SaveChangesAsync();

        return newQuiz.MapToQuizDetailModel();
    }

    public async Task EditQuizAsync(Guid quizId, EditQuizModel editQuizModel)
    {
        var quiz = await dbContext.Quizzes
            .Where(q => q.Id == quizId)
            .Include(q => q.Questions)
            .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync();

        if (quiz == null)
        {
            throw new NotFoundException($"Quiz with id {quizId} not found.");
        }

        // Update existing quiz
        quiz.Title = editQuizModel.Title;

        // Update quiz questions
        UpdateQuestionsAsync(quiz, editQuizModel.Questions.ToList());

        await dbContext.SaveChangesAsync();
    }

    private void UpdateQuestionsAsync(Quiz quiz, List<EditQuestionModel> editQuestionModels)
    {
        var questionIds = editQuestionModels.Select(q => q.Id).ToList();

        // Remove deleted questions
        var questionsToRemove = quiz.Questions.Where(q => !questionIds.Contains(q.Id)).ToList();
        dbContext.Questions.RemoveRange(questionsToRemove);

        // Handle the rest
        var editQuestionModelsToHandle = editQuestionModels.Where(q => questionIds.Contains(q.Id)).ToList();

        foreach (var editQuestionModel in editQuestionModelsToHandle)
        {
            var question = quiz.Questions.FirstOrDefault(q => q.Id == editQuestionModel.Id);

            if (question != null)
            {
                // Update existing question
                question.QuestionText = editQuestionModel.QuestionText;
                question.TimeLimit = editQuestionModel.TimeLimit;

                // Update question answers
                UpdateAnswersAsync(question, editQuestionModel.Answers.ToList());
            }
            else
            {
                // Add new question
                var newQuestion = editQuestionModel.MapToQuestion();
                quiz.Questions.Add(newQuestion);
            }
        }
    }

    private void UpdateAnswersAsync(Question question, List<EditAnswerModel> editAnswerModels)
    {
        var answerIds = editAnswerModels.Select(a => a.Id).ToList();

        // Remove deleted questions
        var answersToRemove = question.Answers.Where(a => !answerIds.Contains(a.Id)).ToList();
        dbContext.Answers.RemoveRange(answersToRemove);

        // Handle the rest
        var editAnswerModelsToHandle = editAnswerModels.Where(a => answerIds.Contains(a.Id)).ToList();

        foreach (var editAnswerModel in editAnswerModelsToHandle)
        {
            var answer = question.Answers.FirstOrDefault(a => a.Id == editAnswerModel.Id);

            if (answer != null)
            {
                // Update existing answer
                answer.AnswerText = editAnswerModel.AnswerText;
                answer.IsCorrect = editAnswerModel.IsCorrect;
            }
            else
            {
                // Add new answer
                var newAnswer = editAnswerModel.MapToAnswer();
                question.Answers.Add(newAnswer);
            }
        }
    }

    public async Task DeleteQuizByIdAsync(Guid quizId)
    {
        var quiz = await dbContext.Quizzes.FindAsync(quizId);
        if (quiz == null)
        {
            throw new NotFoundException($"Quiz with id ${quizId} not found");
        }

        dbContext.Quizzes.Remove(quiz);

        await dbContext.SaveChangesAsync();
    }
}
