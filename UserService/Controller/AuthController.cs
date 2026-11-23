using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.CQRS.AuthService;
using UserService.DTO.Request;
using UserService.DTO.Response;

namespace UserService.Controller;

[ApiController]
[Route("api/Auth")]
public class AuthController(ILogin login, IMediator mediator) : ControllerBase
{
    [HttpPost("Login")]
    public async Task<ActionResult<LoginResponse>> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await login.LoginAsync(request));
    }

    [HttpPost("RefreshToken")]
    public async Task<ActionResult<LoginResponse>> RefreshToken(
        [FromBody] RefreshRequest refreshRequest,
        CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new RefreshCommand(refreshRequest), cancellationToken));
    }
}