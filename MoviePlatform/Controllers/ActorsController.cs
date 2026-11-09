using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviePlatform.CQRS.Actor.Command;
using MoviePlatform.CQRS.Actor.Query;
using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;

namespace MoviePlatform.Controllers;

[ApiController]
[Route("api/actors")]
public class ActorsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ActorResponse>> Create(
        ActorRequest request, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new CreateActorCommand(request), cancellationToken));

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteActorCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<ActorResponse>> GetById(long id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetActorByIdQuery(id), cancellationToken));

    [HttpGet]
    public async Task<ActionResult<List<ActorResponse>>> GetAll(
        int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        => Ok(await mediator.Send(new GetAllActorsQuery(page, pageSize), cancellationToken));
}