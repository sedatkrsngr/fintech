using Fintech.NotificationService.Application.Abstractions;
using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Infrastructure.Providers.Sms;

public sealed class MockSmsProvider : ISmsProvider
{
    public bool CanHandle(string providerKey, NotificationProviderType providerType)
    {
        return providerType == NotificationProviderType.Mock;
    }

    public Task SendAsync(
        Guid providerId,
        string phoneNumber,
        string message,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
