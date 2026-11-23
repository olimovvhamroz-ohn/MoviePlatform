namespace EngagementService.Entities;

public class FavoriteEntity:BaseEntity
{
    public long MovieId { get; set; }
    public long UserId { get; set; }
}