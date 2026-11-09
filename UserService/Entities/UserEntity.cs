namespace UserService.Entities;

public class UserEntity : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string Password { get; set; } = null!;
    public  required DateOnly BirthDate { get; set; }
    public Role Role { get; set; } = Role.User;
    public bool IsBlocked { get; set; }

}