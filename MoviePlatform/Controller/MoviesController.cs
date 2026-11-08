using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviePlatform.CQRS.Movie.Command;
using MoviePlatform.CQRS.Movie.Query;
using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;

namespace MoviePlatform.Controller;

[ApiController]
[Route("api/movies")]
public class MoviesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<MovieResponse>> Create(MovieRequest request)
        => Ok(await mediator.Send(new CreateMovieCommand(request)));
    
    
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteMovieCommand(id), cancellationToken);
        return NoContent();
    }
    
    [HttpGet]
    public async Task<ActionResult<List<MovieResponse>>> GetAll(
        int page = 1, int pageSize = 10,
        long? categoryId = null, long? studioId = null,
        CancellationToken cancellationToken = default)
        => Ok(await mediator.Send(
            new GetAllMovieQuery(page, pageSize, categoryId, studioId), cancellationToken));
}