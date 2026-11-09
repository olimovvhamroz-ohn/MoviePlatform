namespace UserService.DTO.Request;

public class RegisterRequest
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; } 
    public  required DateOnly BirthDate { get; set; }
}