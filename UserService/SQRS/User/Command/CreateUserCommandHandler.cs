using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.DTO.Request;
using UserService.DTO.Response;
using UserService.Entities;
using UserService.Exceptions;
using UserService.Mapping;

namespace UserService.SQRS.User.Command;

public record CreateUserCommand(RegisterRequest Request)
    : IRequest<UserResponse>;

public class CreateUserCommandHandler(
    UserDbContext context,
    IPasswordHasher<UserEntity> passwordHasher)
    : IRequestHandler<CreateUserCommand, UserResponse>
{
    public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var email = request.Request.Email.Trim().ToLowerInvariant();
        var emailChek = await context.Users.AnyAsync(x => x.Email == email);
        if (emailChek)
        {
            throw new ResourceIsAlreadyExistException("This email already exists");
        }

        var entity = UserMapping.ToEntity(request.Request);
        entity.Password = passwordHasher.HashPassword(entity, request.Request.Password);
        context.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return UserMapping.ToResponse(entity);
    }
}