namespace EngagementService.DTO.Request;

public class UpdateReviewRequest
{
    public long MovieId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}