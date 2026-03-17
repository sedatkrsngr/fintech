namespace Fintech.IdentityService.Domain.ValueObjects;

public readonly record struct ApiClientId(Guid Value)
{
    public static ApiClientId New() => new(Guid.NewGuid());

    public static ApiClientId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Api client id cannot be empty.", nameof(value));
        }

        return new ApiClientId(value);
    }
}
