namespace MoviePlatform.Entities;

public class StudioEntity:BaseEntity
{
    public required string  Name { get; set; }
    public  int FoundYear  { get; set; }
    public ICollection<MovieEntity> Movies{ get; set; } = new List<MovieEntity>();
}