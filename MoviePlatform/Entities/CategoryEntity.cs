namespace MoviePlatform.Entities;

public class CategoryEntity:BaseEntity
{
    public required string Title { get; set; } = null!;
    public ICollection<MovieEntity> Movies { get; set; } = new List<MovieEntity>();
    
}