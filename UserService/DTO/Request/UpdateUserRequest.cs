namespace UserService.DTO.Request;

public class UpdateUserRequest
{
    public required string Name { get; set; }
    public required DateOnly BirthDate { get; set; }
}