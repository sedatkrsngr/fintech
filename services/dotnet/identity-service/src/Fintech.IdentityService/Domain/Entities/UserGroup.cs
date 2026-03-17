using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Domain.Entities;

public sealed class UserGroup
{
    private UserGroup(UserId userId, GroupId groupId)
    {
        UserId = userId;
        GroupId = groupId;
        AssignedAtUtc = DateTime.UtcNow;
    }

    public UserId UserId { get; }

    public GroupId GroupId { get; }

    public DateTime AssignedAtUtc { get; }

    public static UserGroup Create(UserId userId, GroupId groupId)
    {
        return new UserGroup(userId, groupId);
    }
}
