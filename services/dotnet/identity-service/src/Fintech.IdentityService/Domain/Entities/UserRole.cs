using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Domain.Entities;

public sealed class UserRole
{
    private UserRole(UserId userId, RoleId roleId)
    {
        UserId = userId;
        RoleId = roleId;
        AssignedAtUtc = DateTime.UtcNow;
    }

    public UserId UserId { get; }

    public RoleId RoleId { get; }

    public DateTime AssignedAtUtc { get; }

    public static UserRole Create(UserId userId, RoleId roleId)
    {
        return new UserRole(userId, roleId);
    }
}
