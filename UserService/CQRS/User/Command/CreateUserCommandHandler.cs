using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.CQRS.AuthService;
using UserService.Data;
using UserService.DTO.Request;
using UserService.DTO.Response;
using UserService.Entities;
using UserService.Exceptions;
using UserService.Mapping;

namespace UserService.CQRS.User.Command;

public record CreateUserCommand(RegisterRequest Request)
    : IRequest<LoginResponse>;

public class CreateUserCommandHandler(
    UserDbContext context,
    IPasswordHasher<UserEntity> passwordHasher,
    IJwtService jwtService,
    ILogger<CreateUserCommandHandler> logger)
    : IRequestHandler<CreateUserCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(CreateUserCommand crateUserCommand, CancellationToken cancellationToken)
    {
        var request = crateUserCommand.Request;
        
        var email = request.Email.Trim().ToLowerInvariant();

        var emailChek = await context.Users.AnyAsync(x => x.Email == email,cancellationToken);

        if (emailChek)
        {
            throw new ResourceIsAlreadyExistException("This email already exists");
        }

        var entity = new UserEntity
        {
            Name = request.Name,
            Email = email,
            Password = string.Empty,
            Role = Role.User,
            BirthDate = request.BirthDate,
            IsBlocked = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        entity.Password = passwordHasher.HashPassword(entity,request.Password);
        context.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogInformation("User {Id} registret", entity.Id);

        return new LoginResponse { Token = jwtService.GenerateToken(entity) };
    }
}