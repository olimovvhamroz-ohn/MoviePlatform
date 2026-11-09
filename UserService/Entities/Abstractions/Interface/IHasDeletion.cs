namespace UserService.Entities.Abstractions.Interface;

public interface IHasDeletion
{
    bool IsDeleted { get; set; }
}