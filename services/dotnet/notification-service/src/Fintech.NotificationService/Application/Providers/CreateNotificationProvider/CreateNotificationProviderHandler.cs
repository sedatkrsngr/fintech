using Fintech.NotificationService.Application.Abstractions;
using Fintech.NotificationService.Domain.Entities;

namespace Fintech.NotificationService.Application.Providers.CreateNotificationProvider;

public sealed class CreateNotificationProviderHandler
{
    private readonly INotificationProviderRepository _notificationProviderRepository;

    public CreateNotificationProviderHandler(INotificationProviderRepository notificationProviderRepository)
    {
        _notificationProviderRepository = notificationProviderRepository;
    }

    public async Task<CreateNotificationProviderResult> HandleAsync(
        CreateNotificationProviderCommand command,
        CancellationToken cancellationToken = default)
    {
        var provider = NotificationProvider.Create(
            command.ProviderKey,
            command.DisplayName,
            command.Channel,
            command.ProviderType,
            command.IsActive);

        await _notificationProviderRepository.AddAsync(provider, cancellationToken);

        return new CreateNotificationProviderResult(
            provider.Id,
            provider.ProviderKey.Value,
            provider.DisplayName,
            provider.Channel,
            provider.ProviderType,
            provider.IsActive,
            provider.CreatedAtUtc);
    }
}
