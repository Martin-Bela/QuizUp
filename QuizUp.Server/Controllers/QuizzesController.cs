using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizUp.BL.Exceptions;
using QuizUp.BL.Services;
using QuizUp.BL.Models;

namespace QuizUp.Server.Controllers;

[Route("api/quizzes")]
[ApiController]
//[Authorize]
public class QuizzesController(IQuizService quizService) : ControllerBase
{
    private ActionResult InternalServerError =>
        StatusCode(StatusCodes.Status500InternalServerError, "Internal server error happened.");

    [HttpGet]
    public async Task<ActionResult<List<QuizSummaryModel>>> GetQuizzesByUserIdAsync([FromQuery] Guid userId)
    {
        try
        {
            var quizSummaryModels = await quizService.GetQuizzessByUserIdAsync(userId);
            return Ok(quizSummaryModels);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch
        {
            return InternalServerError;
        }
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<QuizDetailModel>> GetQuizByIdAsync(Guid id)
    {
        try
        {
            var quizDetailModel = await quizService.GetQuizByIdAsync(id);
            return Ok(quizDetailModel);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch
        {
            return InternalServerError;
        }
    }

    [HttpPost]
    public async Task<ActionResult<QuizDetailModel>> CreateQuizAsync([FromBody] CreateQuizModel createQuizModel)
    {
        try
        {
            var quizDetailModel = await quizService.CreateQuizAsync(createQuizModel);
            return Ok(quizDetailModel);
        }
        catch
        {
            return InternalServerError;
        }
    }

    [HttpPut("{id:Guid}")]
    public async Task<ActionResult> EditQuizAsync(Guid id, [FromBody] EditQuizModel editQuizModel)
    {
        try
        {
            await quizService.EditQuizAsync(id, editQuizModel);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch
        {
            return InternalServerError;
        }
    }

    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> DeleteQuizAsync(Guid id)
    {
        try
        {
            await quizService.DeleteQuizByIdAsync(id);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch
        {
            return InternalServerError;
        }
    }


    [HttpGet("games/{id:Guid}")]
    public async Task<ActionResult<QuizGamesModel>> GetGamesByQuizIdAsync(Guid id)
    {
        try
        {
            var quizGamesModel = await quizService.GetGamesByQuizIdAsync(id);
            return Ok(quizGamesModel);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch
        {
            return InternalServerError;
        }
    }
}
