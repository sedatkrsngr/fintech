namespace Fintech.IdentityService.Domain.ValueObjects;

public readonly record struct GroupId(Guid Value)
{
    public static GroupId New() => new(Guid.NewGuid());

    public static GroupId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Group id cannot be empty.", nameof(value));
        }

        return new GroupId(value);
    }
}
