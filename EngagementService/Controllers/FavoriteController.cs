
using System.Security.Claims;
using EngagementService.CQRS.Favorite.Command;
using EngagementService.CQRS.Favorite.Query;
using EngagementService.DTO.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EngagementService.Controllers;

[ApiController]
[Route("api/favorites")]
[Authorize]
public class FavoriteController(IMediator mediator) : ControllerBase
{
    private long GetUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!long.TryParse(userId, out var id))
            throw new UnauthorizedAccessException("Invalid user ID");

        return id;
    }

    
    [HttpPost]
    public async Task<IActionResult> CreateFavorite(
        [FromBody] FavoriteRequest request)
    {
        var userId = GetUserId();

        var result = await mediator.Send(
            new CreateFavoriteMovieCommand(request, userId));

        return Ok(result);
    }

  
    [HttpGet]
    public async Task<IActionResult> GetMyFavorites(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var userId = GetUserId();

        var result = await mediator.Send(
            new GetMyFavoritesQuery(userId, page, pageSize));

        return Ok(result);
    }
    
    [HttpGet("check/{movieId:long}")]
    public async Task<IActionResult> CheckFavorite(long movieId)
    {
        var userId = GetUserId();

        var result = await mediator.Send(
            new CheckFavoriteQuery(userId, movieId));

        return Ok(result);
    }

    
    [HttpDelete("{movieId:long}")]
    public async Task<IActionResult> DeleteFavorite(long movieId)
    {
        var userId = GetUserId();

        var request = new FavoriteRequest
        {
            MovieId = movieId
        };

        await mediator.Send(
            new DeleteFavoriteMovieCommand(request, userId));

        return NoContent();
    }
}
