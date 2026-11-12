namespace MoviePlatform.Entities;

public class ReviewEntity : BaseEntity
{
    public long UserId { get; set; }
    public long MovieId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
}