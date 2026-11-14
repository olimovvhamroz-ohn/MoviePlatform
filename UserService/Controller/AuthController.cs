using Microsoft.AspNetCore.Mvc;
using UserService.CQRS.AuthService;
using UserService.DTO.Request;
using UserService.DTO.Response;

[ApiController]
[Route("api/Auth")]
public class AuthController(ILogin login) : ControllerBase
{
    [HttpPost("Login")]
    public async Task<ActionResult<LoginResponse>> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await login.LoginAsync(request));
    }
}