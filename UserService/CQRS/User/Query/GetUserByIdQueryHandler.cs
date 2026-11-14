using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.DTO.Response;
using UserService.Exceptions;
using UserService.Mapping;

namespace UserService.CQRS.User.Query;
public record GetUserByIdQuery(long Id) : IRequest<UserResponse>;
public class GetUserByIdQueryHandler(UserDbContext context) : IRequestHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users.AsNoTracking()
                       .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken)
                   ?? throw new ResourceNotFoundException($"User {request.Id} not found.");

        return UserMapping.ToResponse(user);
    }
}