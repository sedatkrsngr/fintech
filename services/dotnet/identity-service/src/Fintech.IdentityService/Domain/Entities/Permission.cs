using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Domain.Entities;

public sealed class Permission
{
    private Permission(PermissionId id, PermissionCode code)
    {
        Id = id;
        Code = code;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public PermissionId Id { get; }

    public PermissionCode Code { get; private set; }

    public DateTime CreatedAtUtc { get; }

    public static Permission Create(PermissionCode code)
    {
        return new Permission(PermissionId.New(), code);
    }

    public void ChangeCode(PermissionCode code)
    {
        Code = code;
    }
}
