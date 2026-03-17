using Fintech.Contracts.Notifications;
using Fintech.NotificationService.Domain.Entities;
using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Application.Abstractions;

public interface INotificationRoutingRuleRepository
{
    Task AddAsync(NotificationRoutingRule routingRule, CancellationToken cancellationToken = default);

    Task<NotificationRoutingRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<NotificationRoutingRule?> GetBestActiveRuleAsync(
        NotificationMessageType messageType,
        NotificationChannel channel,
        CancellationToken cancellationToken = default);
}
