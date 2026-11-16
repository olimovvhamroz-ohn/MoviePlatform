using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.DTO.Request;
using UserService.DTO.Response;
using UserService.Entities;
using UserService.Exceptions;

namespace UserService.CQRS.AuthService;

public interface ILogin
{
    Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
}

public class Login(
    UserDbContext context,
    IJwtService jwtService,
    IPasswordHasher<UserEntity> hasher) : ILogin
{
    public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
    {
        var email = loginRequest.Email.Trim().ToLowerInvariant();

        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email && x.IsBlocked == false);

        if (user is null)
        {
            throw new UnauthorizedException("User  not found with this Email or User is  blocked ");
        }


        var result = hasher.VerifyHashedPassword(user, user.Password, loginRequest.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            throw new UnauthorizedException("Invalid  password");
        }

        if (result == PasswordVerificationResult.SuccessRehashNeeded)
        {
            user.Password = hasher.HashPassword(user, loginRequest.Password);
            await context.SaveChangesAsync();
        }

        var accessToken = jwtService.GenerateToken(user);
        var refreshToken = jwtService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshTokenEntity()
        {
            UserId = user.Id,
            TokenHash = jwtService.HashRefreshToken(refreshToken),
            ExpirationDate = DateTime.UtcNow.AddDays(7)
        };
        context.RefreshToken.Add(refreshTokenEntity);
        await context.SaveChangesAsync();

        return new LoginResponse
        {
            Token = accessToken,
            RefreshToken = refreshToken
        };
    }
}