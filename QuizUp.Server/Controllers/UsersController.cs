using Microsoft.AspNetCore.Mvc;
using QuizUp.BL.Exceptions;
using QuizUp.BL.Services;
using QuizUp.BL.Models;
using Microsoft.AspNetCore.Authorization;

namespace QuizUp.Server.Controllers;

[Route("api/users")]
[ApiController]
[Authorize]
public class UsersController(IUserService userService) : ControllerBase
{
    private ActionResult InternalServerError =>
        StatusCode(StatusCodes.Status500InternalServerError, "Internal server error happened.");

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<UserDetailModel>> RegisterUserAsync([FromBody] CreateUserModel createUserModel)
    {
        try
        {
            var userDetailModel = await userService.CreateUserAsync(createUserModel);
            return Ok(userDetailModel);
        }
        catch (EntityCreationException e)
        {
            return BadRequest(e.Message);
        }
        catch
        {
            return InternalServerError;
        }
    }
}
