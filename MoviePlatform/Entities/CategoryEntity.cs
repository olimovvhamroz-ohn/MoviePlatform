namespace MoviePlatform.Entities;

public class CategoryEntity : BaseEntity
{
    public string Title { get; set; } = null!;
    public ICollection<MovieEntity> Movies { get; set; } = new List<MovieEntity>();
}