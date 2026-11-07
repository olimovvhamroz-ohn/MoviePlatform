using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;
using MoviePlatform.SQRS.Movie.Command;

namespace MoviePlatform.Controller;

[ApiController]
[Route("api/movies")]
public class MoviesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<MovieResponse>> Create(MovieRequest request)
        => Ok(await mediator.Send(new CreateMovieCommand(request)));
}