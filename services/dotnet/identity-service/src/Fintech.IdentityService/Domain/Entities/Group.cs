using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Domain.Entities;

public sealed class Group
{
    private Group(GroupId id, GroupName name)
    {
        Id = id;
        Name = name;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public GroupId Id { get; }

    public GroupName Name { get; private set; }

    public DateTime CreatedAtUtc { get; }

    public static Group Create(GroupName name)
    {
        return new Group(GroupId.New(), name);
    }

    public void Rename(GroupName name)
    {
        Name = name;
    }
}
