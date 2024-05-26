using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizUp.BL.Exceptions;
using QuizUp.BL.Services;
using QuizUp.BL.Models;

namespace QuizUp.Server.Controllers;

[Route("api/games")]
[ApiController]
[Authorize]
public class GamesController(IGameService gameService) : ControllerBase
{
    private ActionResult InternalServerError =>
        StatusCode(StatusCodes.Status500InternalServerError, "Internal server error happened.");

    [HttpGet]
    public async Task<ActionResult<List<GameSummaryModel>>> GetGamesByUserIdAsync([FromQuery] Guid userId)
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
    public async Task<ActionResult<GameResultsModel>> GetGameResultsByIdAsync(Guid id)
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

    //todo remove
    [HttpPost]
    public async Task<ActionResult<CreateGameResultModel>> CreateGameAsync([FromBody] Guid quizId)
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

    //todo remove
    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<SaveGameResultsModel>> SaveGameResultsAsync(Guid id, SaveGameResultsModel saveGameResultsModel)
    {
        try
        {
            await gameService.SaveGameResultsAsync(saveGameResultsModel);
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
    public async Task<ActionResult> DeleteGameAsync(Guid id)
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
