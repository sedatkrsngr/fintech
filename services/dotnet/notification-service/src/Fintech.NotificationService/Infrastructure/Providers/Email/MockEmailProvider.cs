using Fintech.NotificationService.Application.Abstractions;
using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Infrastructure.Providers.Email;

public sealed class MockEmailProvider : IEmailProvider
{
    public bool CanHandle(string providerKey, NotificationProviderType providerType)
    {
        return providerType == NotificationProviderType.Mock;
    }

    public Task SendAsync(
        Guid providerId,
        string toEmail,
        string subject,
        string body,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
