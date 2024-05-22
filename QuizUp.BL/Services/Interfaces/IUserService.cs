using QuizUp.Common.Models;

namespace QuizUp.BL.Services;

public interface IUserService
{
    public Task<UserDetailModel> CreateUserAsync(CreateUserModel createUserModel);
}
