namespace MoviePlatform.DTO.Request;

public class ActorRequest
{
    public required string Name { get; set; }
    public DateOnly  BirthDate { get; set; }
    public string? Biography { get; set; }
}