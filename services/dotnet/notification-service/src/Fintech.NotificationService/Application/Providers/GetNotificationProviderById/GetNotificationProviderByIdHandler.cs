using Fintech.NotificationService.Application.Abstractions;

namespace Fintech.NotificationService.Application.Providers.GetNotificationProviderById;

public sealed class GetNotificationProviderByIdHandler
{
    private readonly INotificationProviderRepository _notificationProviderRepository;

    public GetNotificationProviderByIdHandler(INotificationProviderRepository notificationProviderRepository)
    {
        _notificationProviderRepository = notificationProviderRepository;
    }

    public async Task<GetNotificationProviderByIdResult?> HandleAsync(
        GetNotificationProviderByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var provider = await _notificationProviderRepository.GetByIdAsync(query.Id, cancellationToken);

        if (provider is null)
        {
            return null;
        }

        return new GetNotificationProviderByIdResult(
            provider.Id,
            provider.ProviderKey.Value,
            provider.DisplayName,
            provider.Channel,
            provider.ProviderType,
            provider.IsActive,
            provider.CreatedAtUtc);
    }
}
