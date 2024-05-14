using Microsoft.AspNetCore.Identity;

namespace QuizUp.DAL.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    public ICollection<Game> Games { get; set; } = new List<Game>();

    public ICollection<GameApplicationUser> GameApplicationUsers { get; set; } = new List<GameApplicationUser>();
}
