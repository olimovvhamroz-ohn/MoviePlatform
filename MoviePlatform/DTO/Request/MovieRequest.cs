namespace MoviePlatform.DTO.Request;

public class MovieRequest
{
    public required string Title { get; set; }
    public decimal Price { get; set; }
    public required string Description { get; set; }
    public int DurationInMinutes { get; set; }
    public int ReleaseYear { get; set; }
    public string? PosterUrl { get; set; }
    public long CategoryId { get; set; }
    public long StudioId { get; set; }
}