namespace MoviePlatform.DTO.Request;

public class ReviewRequest
{
    public long UserId { get; set; }  
    public long MovieId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
}