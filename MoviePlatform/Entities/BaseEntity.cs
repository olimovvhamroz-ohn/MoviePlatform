using MoviePlatform.Entities.Abstractions.Interface;

namespace MoviePlatform.Entities;

public class BaseEntity : IHasCreation, IHasModification, IHasDeletion
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}