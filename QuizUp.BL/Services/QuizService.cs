using Microsoft.EntityFrameworkCore;
using QuizUp.BL.Mappers;
using QuizUp.Common.Models;
using QuizUp.DAL.Data;
using QuizUp.DAL.Entities;
using QuizUp.BL.Exceptions;

namespace QuizUp.BL.Services;

public class QuizService(ApplicationDbContext dbContext) : IQuizService
{
    private readonly ApplicationDbContext dbContext = dbContext;

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
        var quiz = await dbContext.Quizzes.FindAsync(quizId);
        if (quiz == null)
        {
            throw new NotFoundException($"Quiz with id ${quizId} not found.");
        }

        return quiz.MapToQuizDetailModel();
    }

    public async Task<QuizDetailModel> CreateQuizAsync(QuizDetailModel quizDetailModel)
    {
        var newQuiz = quizDetailModel.MapToQuiz();
        await dbContext.Quizzes.AddAsync(newQuiz);

        await dbContext.SaveChangesAsync();

        return newQuiz.MapToQuizDetailModel();
    }

    public async Task<QuizDetailModel> EditQuizAsync(QuizDetailModel quizDetailModel)
    {
        if (quizDetailModel.Id == null)
        {
            throw new ArgumentException("Id of quiz to be edited must not be null.");
        }

        var quiz = await dbContext.Quizzes
            .Where(q => q.Id == quizDetailModel.Id)
            .Include(q => q.Questions)
            .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync();

        if (quiz == null)
        {
            throw new NotFoundException($"Quiz with id {quizDetailModel.Id} not found.");
        }

        // Update existing quiz
        quiz.Title = quizDetailModel.Title;

        // Update quiz questions
        UpdateQuestionsAsync(quiz, quizDetailModel.Questions.ToList());

        await dbContext.SaveChangesAsync();

        return quiz.MapToQuizDetailModel();
    }

    private void UpdateQuestionsAsync(Quiz quiz, List<QuestionDetailModel> questionDetailModels)
    {
        var questionIds = questionDetailModels.Select(q => q.Id).ToList();

        // Remove deleted questions
        var questionsToRemove = quiz.Questions.Where(q => !questionIds.Contains(q.Id)).ToList();
        dbContext.Questions.RemoveRange(questionsToRemove);

        // Handle the rest
        var questionDetailModelsToHandle = questionDetailModels.Where(q => questionIds.Contains(q.Id)).ToList();

        foreach (var questionDetailModel in questionDetailModelsToHandle)
        {
            var question = quiz.Questions.FirstOrDefault(q => q.Id == questionDetailModel.Id);

            if (question != null)
            {
                // Update existing question
                question.QuestionText = questionDetailModel.QuestionText;
                question.TimeLimit = questionDetailModel.TimeLimit;

                // Update question answers
                UpdateAnswersAsync(question, questionDetailModel.Answers.ToList());
            }
            else
            {
                // Add new question
                var newQuestion = questionDetailModel.MapToQuestion();
                quiz.Questions.Add(newQuestion);
            }
        }
    }

    private void UpdateAnswersAsync(Question question, List<AnswerDetailModel> answerDetailModels)
    {
        var answerIds = answerDetailModels.Select(a => a.Id).ToList();

        // Remove deleted questions
        var answersToRemove = question.Answers.Where(a => !answerIds.Contains(a.Id)).ToList();
        dbContext.Answers.RemoveRange(answersToRemove);

        // Handle the rest
        var answerDetailModelsToHandle = answerDetailModels.Where(a => answerIds.Contains(a.Id)).ToList();

        foreach (var answerDetailModel in answerDetailModelsToHandle)
        {
            var answer = question.Answers.FirstOrDefault(a => a.Id == answerDetailModel.Id);

            if (answer != null)
            {
                // Update existing answer
                answer.AnswerText = answerDetailModel.AnswerText;
                answer.IsCorrect = answerDetailModel.IsCorrect;
            }
            else
            {
                // Add new answer
                var newAnswer = answerDetailModel.MapToAnswer();
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
