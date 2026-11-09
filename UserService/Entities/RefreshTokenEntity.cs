namespace UserService.Entities;

public class RefreshTokenEntity : BaseEntity
{
    public long UserId { get; set; }
    public UserEntity User { get; set; } = null!;
    public required string TokenHash { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime? RevokedAt { get; set; }
}