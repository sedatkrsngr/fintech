using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Application.Abstractions;

public interface IApiAccessRepository
{
    Task AddApiClientAsync(ApiClient apiClient, CancellationToken cancellationToken = default);

    Task AddApiKeyAsync(ApiKey apiKey, CancellationToken cancellationToken = default);

    Task AddAllowedIpAsync(ApiClientAllowedIp allowedIp, CancellationToken cancellationToken = default);

    Task<ApiClient?> GetApiClientByIdAsync(ApiClientId apiClientId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ApiKey>> GetActiveApiKeysAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ApiClientAllowedIp>> GetAllowedIpsAsync(ApiClientId apiClientId, CancellationToken cancellationToken = default);
}
