using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.DTO.Request;
using UserService.DTO.Response;
using UserService.Entities;
using UserService.Exceptions;

namespace UserService.CQRS.AuthService;

public record RefreshCommand(RefreshRequest Request) : IRequest<LoginResponse>;

public class RefreshCommandHandler(
    UserDbContext context,
    IJwtService jwt,
    IConfiguration configuration) : IRequestHandler<RefreshCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = request.Request.RefreshToken;
        var time = DateTime.UtcNow;
        var hash = jwt.HashRefreshToken(refreshToken);
        var check = await context.RefreshToken
            .FirstOrDefaultAsync(x => x.TokenHash == hash, cancellationToken);

        if (check is null)
        {
            throw new UnauthorizedException("Invalid refresh token");
        }

        if (check.ExpirationDate <= time)
        {
            throw new UnauthorizedException("Refresh token expired");
        }

        if (check.RevokedAt is not null)
        {
            throw new UnauthorizedException("Refresh token has been revoked");
        }

        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == check.UserId && x.IsBlocked == false,
            cancellationToken);

        if (user is null)
        {
            throw new UnauthorizedException("User not found or blocked");
        }

        var accessToken = jwt.GenerateToken(user);
        var newRefreshToken = jwt.GenerateRefreshToken();
        var newHash = jwt.HashRefreshToken(newRefreshToken);
        check.RevokedAt = time;
        var refreshTokenDays = configuration.GetValue<int?>("Jwt:RefreshTokenExpiresInDays")
                               ?? throw new InvalidOperationException("RefreshTokenExpiresInDays is missing");
        
        var refresh = new RefreshTokenEntity()
        {
            UserId = user.Id,
            TokenHash = newHash,
            ExpirationDate = time.AddDays(refreshTokenDays),
            RevokedAt = null
        };
        context.RefreshToken.Add(refresh);
        await context.SaveChangesAsync(cancellationToken);

        return new LoginResponse
        {
            Token = accessToken,
            RefreshToken = newRefreshToken
        };
    }
}