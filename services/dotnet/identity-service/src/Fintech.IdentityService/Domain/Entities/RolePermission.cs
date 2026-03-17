using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Domain.Entities;

public sealed class RolePermission
{
    private RolePermission(RoleId roleId, PermissionId permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
        AssignedAtUtc = DateTime.UtcNow;
    }

    public RoleId RoleId { get; }

    public PermissionId PermissionId { get; }

    public DateTime AssignedAtUtc { get; }

    public static RolePermission Create(RoleId roleId, PermissionId permissionId)
    {
        return new RolePermission(roleId, permissionId);
    }
}
