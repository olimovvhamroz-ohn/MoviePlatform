using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.CQRS.User.Command;
using UserService.CQRS.User.Query;
using UserService.DTO.Request;
using UserService.DTO.Response;

namespace UserService.Controller;

[ApiController]
[Route("api/users")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:long}")]
    public async Task<ActionResult<UserResponse>> GetById(long id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetUserByIdQuery(id), cancellationToken));

    [HttpPost("Register")]
    public async Task<ActionResult<LoginResponse>> Create(RegisterRequest request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new CreateUserCommand(request));
    }

    [Authorize]
    [HttpPut("{id:long}")]
    public async Task<ActionResult<UserResponse>> Update(
        long id, UpdateUserRequest request, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new UpdateUserCommand(id, request.Name, request.BirthDate), cancellationToken));
    }

    [Authorize]
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteUserCommand(id), cancellationToken);
        return NoContent();
    }
}