using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizUp.BL.Exceptions;
using QuizUp.BL.Services;
using QuizUp.Common.Models;

namespace QuizUp.Server.Controllers;

[Route("api/quizzes")]
[ApiController]
[Authorize]
public class QuizzesController(IQuizService quizService) : ControllerBase
{
    private IActionResult InternalServerError =>
        StatusCode(StatusCodes.Status500InternalServerError, "Internal server error happened.");

    [HttpGet]
    public async Task<IActionResult> GetQuizzesByUserId([FromQuery] Guid userId)
    {
        try
        {
            var quizSummaryModels = await quizService.GetQuizzessByUserIdAsync(userId);
            return Ok(quizSummaryModels);
        } catch (NotFoundException e)
        {
            return NotFound(e.Message);
        } catch
        {
            return InternalServerError;
        }
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetQuizById(Guid id)
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
    public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizModel createQuizModel)
    {
        try
        {
            var quizDetailModel = await quizService.CreateQuizAsync(createQuizModel);
            return Ok(quizDetailModel);
        } catch
        {
            return InternalServerError;
        }
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> EditQuiz(Guid id, [FromBody] EditQuizModel editQuizModel)
    {
        try
        {
            await quizService.EditQuizAsync(id, editQuizModel);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        } catch
        {
            return InternalServerError;
        }
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteQuiz(Guid id)
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
}
