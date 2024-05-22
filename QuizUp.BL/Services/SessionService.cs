using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizUp.BL.Exceptions;
using QuizUp.BL.Mappers;
using QuizUp.Common.Models;
using QuizUp.DAL.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuizUp.BL.Services;

public class SessionService(UserManager<ApplicationUser> userManager, IConfiguration configuration) : ISessionService
{
    public async Task<LoginUserResponseModel> LoginUser(LoginUserModel loginUserModel)
    {
        var applicationUser = await userManager.FindByNameAsync(loginUserModel.UserName);
        if (applicationUser == null)
        {
            throw new NotFoundException($"User with username {loginUserModel.UserName} not found.");
        }

        var isPasswordOk = await userManager.CheckPasswordAsync(applicationUser, loginUserModel.Password);
        if (!isPasswordOk)
        {
            throw new WrongPasswordException($"Sent password for user with username {loginUserModel.UserName} is wrong.");
        }

        var token = GenerateJwtToken(loginUserModel);

        return new LoginUserResponseModel()
        {
            User = applicationUser.MapToUserDetailModel(),
            Token = token
        };
    }

    public Task LogoutUser()
    {
        throw new NotImplementedException();
    }

    private string GenerateJwtToken(LoginUserModel loginUserModel)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, loginUserModel.UserName),
        };

        var key = configuration.GetSection("Jwt:Key").Value ?? "";
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            issuer: configuration.GetSection("Jwt:Issuer").Value,
            audience: configuration.GetSection("Jwt:Audience").Value,
            signingCredentials: signingCredentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();

        string token = tokenHandler.WriteToken(securityToken);
        return token;
    }
}
