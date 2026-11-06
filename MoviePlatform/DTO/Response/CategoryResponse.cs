namespace MoviePlatform.DTO.Response;

public class CategoryResponse
{
    
    public required string Title { get; set; } = null!;
    public ICollection<MovieResponse> Movies { get; set; } = new List<MovieResponse>();
}