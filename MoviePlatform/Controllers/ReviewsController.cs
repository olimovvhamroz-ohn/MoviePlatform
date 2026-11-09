using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviePlatform.CQRS.Review;
using MoviePlatform.CQRS.Review.Command;
using MoviePlatform.CQRS.Review.Query;
using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;

namespace MoviePlatform.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ReviewResponse>> Create(
        ReviewRequest request, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new CreateReviewCommand(request), cancellationToken));

    [HttpGet("movie/{movieId:long}")]
    public async Task<ActionResult<List<ReviewResponse>>> GetByMovie(
        long movieId, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        => Ok(await mediator.Send(new GetReviewsByMovieQuery(movieId, page, pageSize), cancellationToken));
}