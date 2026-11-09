namespace UserService.DTO.Response;

public class AuthResponse
{
    public required string AccessToken { get; set; }
    public DateTime AccessTokenExpiresAt { get; set; }
    public required string RefreshToken { get; set; }
}