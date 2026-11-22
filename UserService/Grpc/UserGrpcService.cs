using MagicOnion;
using MagicOnion.Server;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.DTOs;
using UserService.Data;
using UserService.Exceptions;

namespace UserService.Grpc;

public class UserGrpcService(UserDbContext context) : ServiceBase<IUserService>, IUserService
{
    public async UnaryResult<UserDto?> GetUserById(long id)
    {
        var user = await context.Users.AsNoTracking()
                       .FirstOrDefaultAsync(u => u.Id == id)
                   ?? throw new ResourceNotFoundException($"User  not found.");


        var userDto = new UserDto()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            BirthDate = user.BirthDate,
        };
        return userDto;
    }

}