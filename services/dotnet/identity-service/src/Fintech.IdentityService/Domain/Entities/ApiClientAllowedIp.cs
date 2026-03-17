using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Domain.Entities;

public sealed class ApiClientAllowedIp
{
    private ApiClientAllowedIp(ApiClientId apiClientId, AllowedIpAddress ipAddress)
    {
        ApiClientId = apiClientId;
        IpAddress = ipAddress;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public ApiClientId ApiClientId { get; }

    public AllowedIpAddress IpAddress { get; }

    public DateTime CreatedAtUtc { get; }

    public static ApiClientAllowedIp Create(ApiClientId apiClientId, AllowedIpAddress ipAddress)
    {
        return new ApiClientAllowedIp(apiClientId, ipAddress);
    }
}
