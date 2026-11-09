using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviePlatform.CQRS.Studio.Command;
using MoviePlatform.CQRS.Studio.Query;
using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;

namespace MoviePlatform.Controllers;

[ApiController]
[Route("api/studios")]
public class StudiosController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<StudioResponse>> Create(
        StudioRequest request, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new CreateStudioCommand(request), cancellationToken));

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteStudioCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<StudioResponse>> GetById(long id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetStudioByIdQuery(id), cancellationToken));

    [HttpGet]
    public async Task<ActionResult<List<StudioResponse>>> GetAll(
        int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        => Ok(await mediator.Send(new GetAllStudiosQuery(page, pageSize), cancellationToken));
   
}