using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizUp.DAL.Entities;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

namespace QuizUp.DAL.Data;

public static class DataInitializer
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var applicationUsers = PrepareApplicationUsers();
        var quizzes = PrepareQuizzes(applicationUsers);
        var questions = PrepareQuestions(quizzes);
        var answers = PrepareAnswers(questions);
        var games = PrepareGames(quizzes);
        var gameApplicationUsers = PrepareGameApplicationUsers(games, applicationUsers);
        var gameAnswers = PrepareGameAnswers(games, gameApplicationUsers, questions, answers);

        modelBuilder.Entity<ApplicationUser>()
            .HasData(applicationUsers);

        modelBuilder.Entity<Quiz>()
            .HasData(quizzes);

        modelBuilder.Entity<Question>()
            .HasData(questions);

        modelBuilder.Entity<Answer>()
            .HasData(answers);

        modelBuilder.Entity<Game>()
            .HasData(games);

        modelBuilder.Entity<GameApplicationUser>()
            .HasData(gameApplicationUsers);

        modelBuilder.Entity<GameAnswer>()
            .HasData(gameAnswers);
    }

    public static List<ApplicationUser> PrepareApplicationUsers()
    {
        var applicationUsers = new List<ApplicationUser>();
        PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

        foreach (var userData in RandomData.UsersData)
        {
            var applicationUser = new ApplicationUser()
            {
                Id = Guid.NewGuid(),
                UserName = userData.Username,
                NormalizedUserName = userData.Username.ToUpper(),
                Email = userData.Email,
                NormalizedEmail = userData.Email.ToUpper(),
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            applicationUser.PasswordHash = passwordHasher.HashPassword(applicationUser, userData.Password);
            
            applicationUsers.Add(applicationUser);
        }

        return applicationUsers;
    }

    public static List<Quiz> PrepareQuizzes(List<ApplicationUser> applicationUsers)
    {
        var quizzes = new List<Quiz>();
        // random number of quizzes for each user
        int[] quizzesCounts = GetRandomArrayWithSum(applicationUsers.Count, RandomData.QuizzesData.Length);

        Debug.Assert(quizzesCounts.Sum() == RandomData.QuizzesData.Length);

        int quizIndex = 0;

        for (int userIndex = 0; userIndex < applicationUsers.Count; userIndex++)
        {
            for (int i = 0; i < quizzesCounts[userIndex]; i++)
            {
                var quiz = new Quiz()
                {
                    Title = RandomData.QuizzesData[quizIndex].Title,
                    ApplicationUserId = applicationUsers[userIndex].Id,
                };

                quizzes.Add(quiz);
                quizIndex++;
            }
        }

        return quizzes;
    }

    public static List<Question> PrepareQuestions(List<Quiz> quizzes)
    {
        Debug.Assert(quizzes.Count == RandomData.QuizzesData.Length);

        var questions = new List<Question>();

        for (int quizIndex = 0; quizIndex < quizzes.Count; quizIndex++)
        {
            foreach (var questionData in RandomData.QuizzesData[quizIndex].Questions)
            {
                var question = new Question()
                {
                    QuestionText = questionData.QuestionText,
                    TimeLimit = questionData.TimeLimit,
                    QuizId = quizzes[quizIndex].Id
                };

                questions.Add(question);
            }
        }

        return questions;
    }

    public static List<Answer> PrepareAnswers(List<Question> questions)
    {
        var answers = new List<Answer>();
        int questionIndex = 0;

        foreach (var quizData in RandomData.QuizzesData)
        {
            foreach (var questionData in quizData.Questions)
            {
                foreach (var answerData in questionData.Answers)
                {
                    var answer = new Answer()
                    {
                        AnswerText = answerData.AnswerText,
                        IsCorrect = answerData.IsCorrect,
                        QuestionId = questions[questionIndex].Id
                    };

                    answers.Add(answer);
                }

                questionIndex++;
            }
        }

        return answers;
    }

    public static List<Game> PrepareGames(List<Quiz> quizzes)
    {
        int numOfGames = quizzes.Count * 2;

        var games = new List<Game>();
        // random number of games for each quiz
        int[] gamesCounts = GetRandomArrayWithSum(quizzes.Count, numOfGames);

        // code is a six digit number
        var randomCodes = GetUniqueRandomNumbers(numOfGames, 0, 999999);
        var randomCodeIndex = 0;

        for (int quizIndex = 0; quizIndex < quizzes.Count; quizIndex++)
        {
            for (int i = 0; i < gamesCounts[quizIndex]; i++)
            {
                var game = new Game()
                {
                    // game code is a six digits number
                    Code = randomCodes[randomCodeIndex],
                    IsFinished = false,
                    QuizId = quizzes[quizIndex].Id
                };

                games.Add(game);
                randomCodeIndex++;
            }
        }

        return games;
    }

    public static List<GameApplicationUser> PrepareGameApplicationUsers(
        List<Game> games,
        List<ApplicationUser> applicationUsers
    )
    {
        const int minPlayersPerGame = 2;
        Debug.Assert(applicationUsers.Count >= minPlayersPerGame);

        const int maxScore = 1000;

        var gameApplicationUsers = new List<GameApplicationUser>();
        var random = new Random();

        foreach (var game in games)
        {
            int numOfPlayers = random.Next(minPlayersPerGame, applicationUsers.Count);
            var shuffledUsers = applicationUsers.OrderBy(_ => random.Next()).ToList();

            for (int playerIndex = 0; playerIndex < numOfPlayers; playerIndex++)
            {
                var gameApplicationUser = new GameApplicationUser()
                {
                    GameId = game.Id,
                    ApplicationUserId = shuffledUsers[playerIndex].Id,
                    Score = random.Next(0, maxScore + 1)
                };

                gameApplicationUsers.Add(gameApplicationUser);
            }
        }

        return gameApplicationUsers;
    }

    public static List<GameAnswer> PrepareGameAnswers(
        List<Game> games,
        List<GameApplicationUser> gameApplicationUsers,
        List<Question> questions,
        List<Answer> answers
    )
    {
        var gameAnswers = new List<GameAnswer>();

        var gamesQuestionIds = games
            .Join(questions, g => g.QuizId, q => q.QuizId, (g, q) => new { GameId = g.Id, QuestionId = q.Id })
            .GroupBy(joinItem => joinItem.GameId)
            .ToDictionary(group => group.Key, group => group.Select(item => item.QuestionId).ToList());

        var questionsAnswerIds = answers
            .GroupBy(a => a.QuestionId)
            .ToDictionary(group => group.Key, group => group.Select(a => a.Id).ToList());

        var gamesPlayerCounts = gameApplicationUsers
            .GroupBy(gau => gau.GameId)
            .ToDictionary(group => group.Key, group => group.Count());

        var random = new Random();

        foreach (var game in games)
        {
            var gameQuestionIds = gamesQuestionIds[game.Id];
            var gamePlayerCount = gamesPlayerCounts[game.Id];
            
            foreach (var questionId in gameQuestionIds)
            {
                var questionAnswerIds = questionsAnswerIds[questionId];

                // randomly distribute player choices between answers
                var playerChoiceCounts = GetRandomArrayWithSum(questionAnswerIds.Count, gamePlayerCount);

                for (int i = 0; i < playerChoiceCounts.Length; i++)
                {
                    var gameAnswer = new GameAnswer()
                    {
                        GameId = game.Id,
                        AnswerId = questionAnswerIds[i],
                        AnsweredCount = playerChoiceCounts[i]
                    };

                    gameAnswers.Add(gameAnswer);
                }
            }
        }

        return gameAnswers;
    }

    private static int[] GetRandomArrayWithSum(int arrayLength, int arraySum)
    {
        int[] array = new int[arrayLength];
        var random = new Random();

        for (int i = 0; i < arraySum; i++)
        {
            array[random.Next(0, arrayLength)]++;
        }

        return array;
    }

    private static List<int> GetUniqueRandomNumbers(int resultLength, int minNumber, int maxNumber)
    {
        var uniqueRandomNumbers = new HashSet<int>();
        var random = new Random();

        while (uniqueRandomNumbers.Count != resultLength)
        {
            uniqueRandomNumbers.Add(random.Next(minNumber, maxNumber));
        }

        return uniqueRandomNumbers.ToList();
    }
}
