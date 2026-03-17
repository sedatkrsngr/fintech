using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Domain.Entities;

public sealed class ApiClient
{
    private ApiClient(ApiClientId id, ApiClientName name)
    {
        Id = id;
        Name = name;
        IsActive = true;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public ApiClientId Id { get; }

    public ApiClientName Name { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAtUtc { get; }

    public static ApiClient Create(ApiClientName name)
    {
        return new ApiClient(ApiClientId.New(), name);
    }

    public void Rename(ApiClientName name)
    {
        Name = name;
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
