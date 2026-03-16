using Fintech.NotificationService.Domain.Entities;

namespace Fintech.NotificationService.Application.Abstractions;

public interface INotificationRoutingRuleRepository
{
    Task AddAsync(NotificationRoutingRule routingRule, CancellationToken cancellationToken = default);

    Task<NotificationRoutingRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
