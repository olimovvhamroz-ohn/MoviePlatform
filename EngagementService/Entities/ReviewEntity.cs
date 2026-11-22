namespace EngagementService.Entities;

public class ReviewEntity:BaseEntity
{
    public long MovieId { get; set; }
    public long UserId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    
}