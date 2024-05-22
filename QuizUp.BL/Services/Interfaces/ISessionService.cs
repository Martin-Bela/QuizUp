using QuizUp.Common.Models;

namespace QuizUp.BL.Services;

public interface ISessionService
{
    public Task<LoginUserResponseModel> LoginUser(LoginUserModel loginUserModel);

    public Task LogoutUser();
}
