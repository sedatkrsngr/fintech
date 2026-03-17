using Fintech.Contracts.Notifications;

namespace Fintech.IdentityService.Application.Abstractions;

public interface INotificationDispatchClient
{
    Task SendEmailAsync(
        NotificationMessageType messageType,
        string toEmail,
        string subject,
        string body,
        CancellationToken cancellationToken = default);
}
