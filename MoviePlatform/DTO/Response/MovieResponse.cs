using MoviePlatform.Entities;
using MoviePlatform.Entities.Enums;

namespace MoviePlatform.DTO.Response;

public class MovieResponse
{

    public required string Title { get; set; }
    public decimal Price { get; set; }
    public required string Description { get; set; }
    public int DurationInMinutes { get; set; }
    public int ReleaseYear { get; set; }
    public AgeRating AgeRating { get; set; }
    public string? PosterUrl { get; set; }

    public long CategoryId { get; set; }
    public CategoryResponse Category { get; set; } = null!;

    public ICollection<ActorResponse> Actors { get; set; } = new List<ActorResponse>();

    public long StudioId { get; set; }
    public StudioResponse Studio { get; set; } = null!;
}

