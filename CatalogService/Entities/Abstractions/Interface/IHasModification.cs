namespace MoviePlatform.Entities.Abstractions.Interface;

public interface IHasModification
{
    DateTime UpdatedAt { get; set; }
}