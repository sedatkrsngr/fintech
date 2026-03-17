using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Application.ApiAccess.CreateApiClient;

public sealed class CreateApiClientHandler
{
    private readonly IApiAccessRepository _apiAccessRepository;
    private readonly IPasswordHashingService _passwordHashingService;

    public CreateApiClientHandler(
        IApiAccessRepository apiAccessRepository,
        IPasswordHashingService passwordHashingService)
    {
        _apiAccessRepository = apiAccessRepository;
        _passwordHashingService = passwordHashingService;
    }

    public async Task<CreateApiClientResult> HandleAsync(
        CreateApiClientCommand command,
        CancellationToken cancellationToken = default)
    {
        var apiClient = ApiClient.Create(ApiClientName.Create(command.Name));
        await _apiAccessRepository.AddApiClientAsync(apiClient, cancellationToken);

        var plainApiKey = $"ftk_{Guid.NewGuid():N}";
        var hashedApiKey = HashedApiKey.Create(_passwordHashingService.HashPassword(plainApiKey));
        var apiKey = ApiKey.Create(apiClient.Id, hashedApiKey);
        await _apiAccessRepository.AddApiKeyAsync(apiKey, cancellationToken);

        var allowedIps = new List<string>();

        foreach (var ip in command.AllowedIps.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var allowedIp = ApiClientAllowedIp.Create(apiClient.Id, AllowedIpAddress.Create(ip));
            await _apiAccessRepository.AddAllowedIpAsync(allowedIp, cancellationToken);
            allowedIps.Add(allowedIp.IpAddress.Value);
        }

        return new CreateApiClientResult(
            apiClient.Id.Value,
            apiClient.Name.Value,
            plainApiKey,
            apiClient.CreatedAtUtc,
            allowedIps);
    }
}
