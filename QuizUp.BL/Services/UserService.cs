using Microsoft.AspNetCore.Identity;
using QuizUp.BL.Exceptions;
using QuizUp.BL.Mappers;
using QuizUp.BL.Models;
using QuizUp.DAL.Entities;

namespace QuizUp.BL.Services;

public class UserService(UserManager<ApplicationUser> userManager) : IUserService
{
    public async Task<UserDetailModel> CreateUserAsync(CreateUserModel registerUserModel)
    {
        var applicationUser = new ApplicationUser()
        {
            UserName = registerUserModel.Username,
            Email = registerUserModel.Email
        };

        var result = await userManager.CreateAsync(applicationUser, registerUserModel.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join("\n", result.Errors.Select(e => e.Description));
            throw new EntityCreationException($"User creation failed: {errors}");
        }

        return applicationUser.MapToUserDetailModel();
    }
}
