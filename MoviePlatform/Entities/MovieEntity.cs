using MoviePlatform.Entities.Enums;

namespace MoviePlatform.Entities;
public class MovieEntity : BaseEntity
{
    public required string Title { get; set; } 
    public decimal Price { get; set; }
    public required string Description { get; set; } 
    public int  DurationInMinutes  { get; set; }
    public int ReleaseYear  { get; set; }
    public AgeRating AgeRating { get; set; } = AgeRating.Default;
    public string? PosterUrl { get; set; }
    public long CategoryId { get; set; }
    public CategoryEntity Category { get; set; } = null!;
    public ICollection<ActorEntity> Actors { get; set; } = new List<ActorEntity>();
    public long StudioId { get; set; }
    public StudioEntity Studio { get; set; } = null!;
}