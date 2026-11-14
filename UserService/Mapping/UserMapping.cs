using UserService.DTO.Request;
using UserService.DTO.Response;
using UserService.Entities;

namespace UserService.Mapping;

public class UserMapping
{
    public static UserEntity ToEntity(RegisterRequest request)
    {
        return new UserEntity
        {
            Name = request.Name,
            Email = request.Email,
            Password = string.Empty,
            Role = Role.User,
            BirthDate = request.BirthDate,
            IsBlocked = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static UserResponse ToResponse(UserEntity entity)
    {
       return new UserResponse()
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            BirthDate = entity.BirthDate,
            Role = entity.Role,
            IsBlocked = entity.IsBlocked
        };
    }
    
}