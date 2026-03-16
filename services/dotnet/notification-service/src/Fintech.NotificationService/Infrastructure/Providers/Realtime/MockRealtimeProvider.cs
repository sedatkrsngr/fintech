using Fintech.NotificationService.Application.Abstractions;
using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Infrastructure.Providers.Realtime;

public sealed class MockRealtimeProvider : IRealtimeProvider
{
    public bool CanHandle(string providerKey, NotificationProviderType providerType)
    {
        return providerType == NotificationProviderType.Mock;
    }

    public Task SendAsync(
        Guid providerId,
        string targetUserId,
        string payload,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
