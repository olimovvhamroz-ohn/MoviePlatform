namespace UserService.DTO.Response;

public class UserResponse
{
    public required string Name { get; set; }
    public required DateOnly BirthDate { get; set; }
}