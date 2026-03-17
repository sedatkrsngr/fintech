namespace Fintech.IdentityService.Domain.ValueObjects;

public readonly record struct AccessRuleId(Guid Value)
{
    public static AccessRuleId New() => new(Guid.NewGuid());

    public static AccessRuleId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Access rule id cannot be empty.", nameof(value));
        }

        return new AccessRuleId(value);
    }
}
