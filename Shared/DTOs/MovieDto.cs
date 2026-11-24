using MessagePack;

namespace Shared.DTOs;

[MessagePackObject]
public class MovieDto
{
    [Key(0)] public long Id { get; set; }
    [Key(1)] public required string Title { get; set; }
    [Key(2)] public decimal Price { get; set; }
    [Key(3)] public required string Description { get; set; }
    [Key(4)] public int DurationInMinutes { get; set; }
    [Key(5)] public int ReleaseYear { get; set; }
    [Key(6)] public string? PosterUrl { get; set; }
    [Key(7)] public decimal AverageRating { get; set; }
    [Key(8)] public int ReviewsCount { get; set; }
    [Key(9)] public long CategoryId { get; set; }
    [Key(10)] public long StudioId { get; set; }
}