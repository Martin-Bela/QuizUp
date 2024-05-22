using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizUp.BL.Exceptions;
using QuizUp.BL.Services;
using QuizUp.Common.Models;

namespace QuizUp.Server.Controllers;

[Route("api/games")]
[ApiController]
[Authorize]
public class GamesController(IGameService gameService) : ControllerBase
{
    private IActionResult InternalServerError =>
        StatusCode(StatusCodes.Status500InternalServerError, "Internal server error happened.");

    [HttpGet]
    public async Task<IActionResult> GetGamesByUserIdAsync([FromQuery] Guid userId)
    {
        try
        {
            var gameSummaryModels = await gameService.GetGamesByUserIdAsync(userId);
            return Ok(gameSummaryModels);
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
    public async Task<IActionResult> GetGameResultsById(Guid id)
    {
        try
        {
            var gameResultsModel = await gameService.GetGameResultsByIdAsync(id);
            return Ok(gameResultsModel);
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
    public async Task<IActionResult> CreateGame([FromBody] Guid quizId)
    {
        try
        {
            var createGameResultModel = await gameService.CreateGameAsync(quizId);
            return Ok(createGameResultModel);
        }
        catch
        {
            return InternalServerError;
        }
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> SaveGameResults(Guid id, SaveGameResultsModel saveGameResultsModel)
    {
        try
        {
            await gameService.SaveGameResultsAsync(id, saveGameResultsModel);
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
    public async Task<IActionResult> DeleteGame(Guid id)
    {
        try
        {
            await gameService.DeleteGameByIdAsync(id);
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
