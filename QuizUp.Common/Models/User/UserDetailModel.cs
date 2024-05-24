namespace QuizUp.Common.Models;

public class UserDetailModel : ModelBase
{
    public Guid Id { get; set; }

    public required string UserName { get; set; }
    
    public required string Email { get; set; }
}
