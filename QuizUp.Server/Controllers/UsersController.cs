using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizUp.BL.Exceptions;
using QuizUp.BL.Services;
using QuizUp.Common.Models;

namespace QuizUp.Server.Controllers;

[Route("api/users")]
[ApiController]
[Authorize]
public class UsersController(IUserService userService) : ControllerBase
{
    private IActionResult InternalServerError =>
        StatusCode(StatusCodes.Status500InternalServerError, "Internal server error happened.");

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] CreateUserModel createUserModel)
    {
        try
        {
            var userDetailModel = await userService.CreateUserAsync(createUserModel);
            return Ok(userDetailModel);
        } catch (EntityCreationException e)
        {
            return BadRequest(e.Message);
        } catch
        {
            return InternalServerError;
        }
    }
}
