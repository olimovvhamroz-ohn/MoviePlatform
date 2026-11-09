namespace MoviePlatform.DTO.Response;

public class ActorResponse
{
    public required string Name { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? Biography { get; set; }
}