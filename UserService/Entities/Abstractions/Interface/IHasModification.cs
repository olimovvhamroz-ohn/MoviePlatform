namespace UserService.Entities.Abstractions.Interface;

public interface IHasModification
{
    DateTime UpdatedAt { get; set; }
}