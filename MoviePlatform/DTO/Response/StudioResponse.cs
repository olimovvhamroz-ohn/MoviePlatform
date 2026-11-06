namespace MoviePlatform.DTO.Response;

public class StudioResponse
{
    public required string  Name { get; set; }
    public  int FoundYear  { get; set; }
    public ICollection<MovieResponse> Movies{ get; set; } = new List<MovieResponse>();
}