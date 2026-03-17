using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Application.ApiAccess.ValidateApiKey;

public sealed class ValidateApiKeyHandler
{
    private readonly IApiAccessRepository _apiAccessRepository;
    private readonly IPasswordHashingService _passwordHashingService;

    public ValidateApiKeyHandler(
        IApiAccessRepository apiAccessRepository,
        IPasswordHashingService passwordHashingService)
    {
        _apiAccessRepository = apiAccessRepository;
        _passwordHashingService = passwordHashingService;
    }

    public async Task<ValidateApiKeyResult?> HandleAsync(
        ValidateApiKeyCommand command,
        CancellationToken cancellationToken = default)
    {
        var apiKeys = await _apiAccessRepository.GetActiveApiKeysAsync(cancellationToken);

        foreach (var apiKey in apiKeys)
        {
            var verified = _passwordHashingService.VerifyPassword(apiKey.HashedKey.Value, command.ApiKey);

            if (!verified)
            {
                continue;
            }

            var apiClient = await _apiAccessRepository.GetApiClientByIdAsync(apiKey.ApiClientId, cancellationToken);

            if (apiClient is null || !apiClient.IsActive)
            {
                return null;
            }

            if (apiKey.ExpiresAtUtc.HasValue && apiKey.ExpiresAtUtc.Value <= DateTime.UtcNow)
            {
                return null;
            }

            var allowedIps = await _apiAccessRepository.GetAllowedIpsAsync(apiClient.Id, cancellationToken);

            if (allowedIps.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(command.RemoteIp))
                {
                    return null;
                }

                var remoteIp = AllowedIpAddress.Create(command.RemoteIp);
                var ipAllowed = allowedIps.Any(x => x.IpAddress == remoteIp);

                if (!ipAllowed)
                {
                    return null;
                }
            }

            return new ValidateApiKeyResult(apiClient.Id.Value, apiClient.Name.Value);
        }

        return null;
    }
}
