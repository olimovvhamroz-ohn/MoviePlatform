using MoviePlatform.Entities.Enums;

namespace MoviePlatform.DTO.Request;

public class MovieRequest
{
    public required string Title { get; set; } = null!;
    public decimal Price { get; set; }
    public required string Description { get; set; } = null!;
    public int DurationInMinutes { get; set; }
    public int ReleaseYear { get; set; }
    public AgeRating AgeRating { get; set; }
    public string? PosterUrl { get; set; }
    public long CategoryId { get; set; }
    public long StudioId { get; set; }
    public List<long> ActorId { get; set; } = new();
}