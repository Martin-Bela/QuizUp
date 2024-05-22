using Microsoft.AspNetCore.Mvc;
using QuizUp.BL.Exceptions;
using QuizUp.BL.Services;
using QuizUp.Common.Models;

namespace QuizUp.Server.Controllers;

[Route("api/session")]
[ApiController]
public class SessionController(ISessionService sessionService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserModel loginUserModel)
    {
        try
        {
            var loginUserResponseModel = await sessionService.LoginUser(loginUserModel);
            return Ok(loginUserResponseModel);
        } catch (NotFoundException e)
        {
            return NotFound(e.Message);
        } catch (WrongPasswordException e)
        {
            return new UnauthorizedObjectResult(e.Message);
        } catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error happened.");
        }
    }

    [HttpDelete]
    public async Task LogoutUser()
    {

    }
}
