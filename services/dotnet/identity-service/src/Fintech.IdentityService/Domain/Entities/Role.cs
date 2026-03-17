using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Domain.Entities;

public sealed class Role
{
    private Role(RoleId id, RoleName name)
    {
        Id = id;
        Name = name;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public RoleId Id { get; }

    public RoleName Name { get; private set; }

    public DateTime CreatedAtUtc { get; }

    public static Role Create(RoleName name)
    {
        return new Role(RoleId.New(), name);
    }

    public void Rename(RoleName name)
    {
        Name = name;
    }
}
