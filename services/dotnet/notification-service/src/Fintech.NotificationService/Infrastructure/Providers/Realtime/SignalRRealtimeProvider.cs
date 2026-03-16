using Fintech.NotificationService.Application.Abstractions;
using Fintech.NotificationService.Domain.Enums;
using Microsoft.AspNetCore.SignalR;

namespace Fintech.NotificationService.Infrastructure.Providers.Realtime;

public sealed class SignalRRealtimeProvider : IRealtimeProvider
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public SignalRRealtimeProvider(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public bool CanHandle(string providerKey, NotificationProviderType providerType)
    {
        return providerType == NotificationProviderType.SignalR
            || string.Equals(providerKey, "signalr-default", StringComparison.OrdinalIgnoreCase);
    }

    public Task SendAsync(
        Guid providerId,
        string targetUserId,
        string payload,
        CancellationToken cancellationToken = default)
    {
        return _hubContext.Clients.User(targetUserId)
            .SendAsync("notification", payload, cancellationToken);
    }
}
