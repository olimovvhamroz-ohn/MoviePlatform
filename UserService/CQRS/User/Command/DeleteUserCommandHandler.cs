using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Exceptions;

namespace UserService.CQRS.User.Command;

public record DeleteUserCommand(long Id) : IRequest<bool>;

public class DeleteUserCommandHandler(
    UserDbContext context,
    ILogger<DeleteUserCommandHandler> logger) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users
                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                   ?? throw new ResourceNotFoundException($"User with id {request.Id} not found");

        context.Users.Remove(user);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("User {Id} deleted", request.Id);
        return true;
    }
}