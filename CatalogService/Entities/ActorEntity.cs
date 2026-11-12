namespace MoviePlatform.Entities;

public class ActorEntity : BaseEntity
{
    public required string Name { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? Biography { get; set; }
}