namespace QuizUp.Common.Models;

public class CreateUserModel
{
    public required string Username { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}
