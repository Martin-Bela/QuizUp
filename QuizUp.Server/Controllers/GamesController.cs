using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizUp.BL.Exceptions;
using QuizUp.BL.Services;
using QuizUp.BL.Models;
using System.Security.Claims;

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
        var accessTokenUserId = GetAccessTokenUserId();
        if (accessTokenUserId == null || accessTokenUserId != userId)
        {
            return Unauthorized();
        }

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
        var gameBelongsToUser = await DoesGameBelongToUser(id);
        if (!gameBelongsToUser)
        {
            return Unauthorized();
        }

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
        var gameBelongsToUser = await DoesGameBelongToUser(id);
        if (!gameBelongsToUser)
        {
            return Unauthorized();
        }

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

    private Guid? GetAccessTokenUserId()
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdString == null)
        {
            return null;
        }

        return new Guid(userIdString);
    }

    private async Task<bool> DoesGameBelongToUser(Guid gameId)
    {
        var userId = GetAccessTokenUserId();
        if (userId == null)
        {
            return false;
        }

        return await gameService.DoesGameBelongToUser(gameId, userId ?? Guid.Empty);
    }
}
