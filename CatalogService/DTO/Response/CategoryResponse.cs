namespace MoviePlatform.DTO.Response;

public class CategoryResponse
{
    public long Id { get; set; }
    public required string Title { get; set; } = null!;
}