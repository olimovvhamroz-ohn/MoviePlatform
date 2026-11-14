using UserService.Entities;

namespace UserService.DTO.Response;

public class UserResponse
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required DateOnly BirthDate { get; set; }
    public Role Role { get; set; }
    public bool IsBlocked { get; set; }
}