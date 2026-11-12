using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.DTO.Request;
using UserService.DTO.Response;
using UserService.SQRS.User.Command;
using UserService.SQRS.User.Query;

namespace UserService.Controller;

[ApiController]
[Route("api/users")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:long}")]
    public async Task<ActionResult<UserResponse>> GetById(long id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetUserByIdQuery(id), cancellationToken));

    [HttpPost]
    public async Task<ActionResult<UserResponse>> Create(RegisterRequest request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new CreateUserCommand(request));
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<UserResponse>> Update(
        long id, UpdateUserRequest request, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new UpdateUserCommand(id, request.Name, request.BirthDate), cancellationToken));
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteUserCommand(id), cancellationToken);
        return NoContent();
    }
}