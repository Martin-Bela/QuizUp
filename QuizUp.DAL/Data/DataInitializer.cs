using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizUp.DAL.Entities;
using System.Diagnostics;

namespace QuizUp.DAL.Data;

public static class DataInitializer
{
    public static class DataConstants
    {
        public static readonly (string, string, string)[] UsersData =
        [
            ("bookworm123", "pavel.novak@seznam.cz", "someRandomPassword123"),
            ("techgeek99", "jane.doe@gmail.com", "TechGeek*456"),
            ("traveler7", "john.smith@outlook.com", "Travel@789"),
            ("foodie256", "mary.jones@gmail.com", "FoodLover!321")
        ];

        public static readonly string[] QuizTitles =
        [
            "Intro to Quantum Physics", "Advanced JavaScript Techniques", "History of the Roman Empire", "Mastering Python",
            "Basics of Digital Marketing", "Art of Storytelling", "World Geography Challenge", "Cybersecurity Fundamentals",
            "Shakespeare's Greatest Works", "Astronomy for Beginners", "Modern Art Movements", "Philosophy 101",
            "Ultimate Cooking Quiz", "Music Theory Essentials", "Environmental Science Facts", "Space Exploration Trivia",
            "Literary Classics Quiz", "Cryptography and Security", "Essential Business Strategies", "Medical Terminology Basics"
        ];
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        var applicationUsers = PrepareApplicationUsers();
        var quizzes = PrepareQuizzes(applicationUsers);

        modelBuilder.Entity<ApplicationUser>()
            .HasData(applicationUsers);

        modelBuilder.Entity<Quiz>()
            .HasData(quizzes);
    }

    public static List<ApplicationUser> PrepareApplicationUsers()
    {
        var applicationUsers = new List<ApplicationUser>();
        PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

        foreach (var userData in DataConstants.UsersData)
        {
            var user = new ApplicationUser()
            {
                Id = Guid.NewGuid(),
                UserName = userData.Item1,
                NormalizedUserName = userData.Item1.ToUpper(),
                Email = userData.Item2,
                NormalizedEmail = userData.Item2.ToUpper(),
            };

            user.PasswordHash = passwordHasher.HashPassword(user, userData.Item3);
            applicationUsers.Add(user);
        }

        return applicationUsers;
    }

    public static List<Quiz> PrepareQuizzes(List<ApplicationUser> applicationUsers)
    {
        int numOfUsers = applicationUsers.Count;
        int numOfTitles = DataConstants.QuizTitles.Length;

        var quizzes = new List<Quiz>();
        // number of quizzes for each user
        int[] quizzesCounts = getRandomArrayWithSum(numOfUsers, numOfTitles);

        Debug.Assert(quizzesCounts.Sum() == numOfTitles);

        int titleIndex = 0;

        for (int i = 0; i < numOfUsers; i++)
        {
            for (int j = 0; j < quizzesCounts[i]; j++)
            {
                var quiz = new Quiz()
                {
                    Title = DataConstants.QuizTitles[titleIndex],
                    ApplicationUserId = applicationUsers[i].Id,
                };

                quizzes.Add(quiz);
                titleIndex++;
            }
        }

        return quizzes;
    }

    private static int[] getRandomArrayWithSum(int arrayLength, int arraySum)
    {
        int[] array = new int[arrayLength];
        var random = new Random();

        for (int i = 0; i < arraySum; i++)
        {
            array[random.Next(0, arrayLength)]++;
        }

        return array;
    }
}
