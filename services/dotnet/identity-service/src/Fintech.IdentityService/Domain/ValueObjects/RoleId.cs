namespace Fintech.IdentityService.Domain.ValueObjects;

public readonly record struct RoleId(Guid Value)
{
    public static RoleId New() => new(Guid.NewGuid());

    public static RoleId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Role id cannot be empty.", nameof(value));
        }

        return new RoleId(value);
    }
}
