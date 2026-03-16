using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Application.Abstractions;

public interface IEmailProvider
{
    bool CanHandle(string providerKey, NotificationProviderType providerType);

    Task SendAsync(
        Guid providerId,
        string toEmail,
        string subject,
        string body,
        CancellationToken cancellationToken = default);
}
