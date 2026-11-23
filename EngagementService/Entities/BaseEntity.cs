using EngagementService.Entities.Abstractions;

namespace EngagementService.Entities;

public class BaseEntity : IHasCreation, IHasModification, IHasDeletion
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
