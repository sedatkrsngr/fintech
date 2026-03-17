namespace Fintech.IdentityService.Domain.ValueObjects;

public readonly record struct ApiKeyId(Guid Value)
{
    public static ApiKeyId New() => new(Guid.NewGuid());

    public static ApiKeyId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Api key id cannot be empty.", nameof(value));
        }

        return new ApiKeyId(value);
    }
}
