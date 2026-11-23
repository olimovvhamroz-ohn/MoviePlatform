
using System.Security.Claims;
using EngagementService.CQRS.Review.Command;
using EngagementService.CQRS.Review.Query;
using EngagementService.DTO.Request;
using EngagementService.DTO.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EngagementService.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewsController(IMediator mediator) : ControllerBase
{
    private long GetUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!long.TryParse(userId, out var id))
            throw new UnauthorizedAccessException("Invalid user ID");

        return id;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ReviewResponse>> Create(
        [FromBody] ReviewRequest request,
        CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!long.TryParse(userId, out var id))
            return Unauthorized();

        var result = await mediator.Send(
            new CreateReviewCommand(request, id),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("movie/{movieId:long}")]
    public async Task<ActionResult<List<ReviewResponse>>> GetByMovie(
        long movieId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(
            new GetReviewsByMovieQuery(movieId, page, pageSize),
            cancellationToken);

        return Ok(result);
    }

    [Authorize]
    [HttpGet("my")]
    public async Task<ActionResult<List<ReviewResponse>>> GetMyReviews(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var userId = GetUserId();

        var result = await mediator.Send(
            new GetMyReviewsQuery(userId, page, pageSize),
            cancellationToken);

        return Ok(result);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<ReviewResponse>> Update(
        [FromBody] UpdateReviewRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();

        var result = await mediator.Send(
            new UpdateReviewCommand(request, userId),
            cancellationToken);

        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{movieId:long}")]
    public async Task<IActionResult> Delete(
        long movieId,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();

        await mediator.Send(
            new DeleteReviewCommand(movieId, userId),
            cancellationToken);

        return NoContent();
    }
}
