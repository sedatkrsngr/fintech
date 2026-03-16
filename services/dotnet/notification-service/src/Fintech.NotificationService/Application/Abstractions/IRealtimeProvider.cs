using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Application.Abstractions;

public interface IRealtimeProvider
{
    bool CanHandle(string providerKey, NotificationProviderType providerType);

    Task SendAsync(
        Guid providerId,
        string targetUserId,
        string payload,
        CancellationToken cancellationToken = default);
}
