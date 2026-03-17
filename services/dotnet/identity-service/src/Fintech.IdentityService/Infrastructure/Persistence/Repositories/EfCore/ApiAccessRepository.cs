using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Fintech.IdentityService.Infrastructure.Persistence.Repositories.EfCore;

public sealed class ApiAccessRepository : IApiAccessRepository
{
    private readonly IdentityDbContext _dbContext;

    public ApiAccessRepository(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddApiClientAsync(ApiClient apiClient, CancellationToken cancellationToken = default)
    {
        await _dbContext.ApiClients.AddAsync(apiClient, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddApiKeyAsync(ApiKey apiKey, CancellationToken cancellationToken = default)
    {
        await _dbContext.ApiKeys.AddAsync(apiKey, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAllowedIpAsync(ApiClientAllowedIp allowedIp, CancellationToken cancellationToken = default)
    {
        await _dbContext.ApiClientAllowedIps.AddAsync(allowedIp, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<ApiClient?> GetApiClientByIdAsync(ApiClientId apiClientId, CancellationToken cancellationToken = default)
    {
        return _dbContext.ApiClients
            .FirstOrDefaultAsync(x => x.Id == apiClientId, cancellationToken);
    }

    public async Task<IReadOnlyList<ApiKey>> GetActiveApiKeysAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.ApiKeys
            .Where(x => x.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ApiClientAllowedIp>> GetAllowedIpsAsync(ApiClientId apiClientId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ApiClientAllowedIps
            .Where(x => x.ApiClientId == apiClientId)
            .ToListAsync(cancellationToken);
    }
}
