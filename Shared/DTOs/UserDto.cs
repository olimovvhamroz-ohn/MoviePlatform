using MessagePack;

namespace Shared.DTOs;

[MessagePackObject]
public class UserDto
{
    [Key(0)]
    public long Id { get; set; }

    [Key(1)]
    public string Name { get; set; } = null!;

    [Key(2)]
    public string Email { get; set; } = null!;

    [Key(3)]
    public DateOnly BirthDate { get; set; }
}