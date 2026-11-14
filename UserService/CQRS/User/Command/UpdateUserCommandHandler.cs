using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.DTO.Response;
using UserService.Exceptions;
using UserService.Mapping;

namespace UserService.CQRS.User.Command;

public record UpdateUserCommand(long Id, string Name, DateOnly BirthDate) : IRequest<UserResponse>;

public class UpdateUserCommandHandler(UserDbContext context) : IRequestHandler<UpdateUserCommand, UserResponse>
{
    public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken)
                   ?? throw new ResourceNotFoundException($"User {request.Id} not found.");

        user.Name = request.Name;
        user.BirthDate = request.BirthDate;
        user.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);
        return UserMapping.ToResponse(user);
    }
}