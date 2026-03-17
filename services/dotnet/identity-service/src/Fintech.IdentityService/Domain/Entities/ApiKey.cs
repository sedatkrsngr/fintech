using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Domain.Entities;

public sealed class ApiKey
{
    private ApiKey(ApiKeyId id, ApiClientId apiClientId, HashedApiKey hashedKey, DateTime? expiresAtUtc)
    {
        Id = id;
        ApiClientId = apiClientId;
        HashedKey = hashedKey;
        ExpiresAtUtc = expiresAtUtc;
        IsActive = true;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public ApiKeyId Id { get; }

    public ApiClientId ApiClientId { get; }

    public HashedApiKey HashedKey { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime? ExpiresAtUtc { get; private set; }

    public DateTime CreatedAtUtc { get; }

    public static ApiKey Create(ApiClientId apiClientId, HashedApiKey hashedKey, DateTime? expiresAtUtc = null)
    {
        return new ApiKey(ApiKeyId.New(), apiClientId, hashedKey, expiresAtUtc);
    }

    public void Rotate(HashedApiKey hashedKey, DateTime? expiresAtUtc = null)
    {
        HashedKey = hashedKey;
        ExpiresAtUtc = expiresAtUtc;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
