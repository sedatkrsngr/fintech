using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Application.Abstractions;

public interface ISmsProvider
{
    bool CanHandle(string providerKey, NotificationProviderType providerType);

    Task SendAsync(
        Guid providerId,
        string phoneNumber,
        string message,
        CancellationToken cancellationToken = default);
}
