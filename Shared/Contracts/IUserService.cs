using MagicOnion;
using Shared.DTOs;

namespace Shared.Contracts;

public interface IUserService:IService<IUserService>{
    UnaryResult<UserDto?> GetUserById(long id);
    
}