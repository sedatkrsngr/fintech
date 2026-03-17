using Fintech.Contracts.Notifications;
using Fintech.NotificationService.Application.Abstractions;
using Fintech.NotificationService.Domain.Entities;
using Fintech.NotificationService.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Fintech.NotificationService.Infrastructure.Persistence.Repositories.EfCore;

public sealed class NotificationRoutingRuleRepository : INotificationRoutingRuleRepository
{
    private readonly NotificationDbContext _dbContext;

    public NotificationRoutingRuleRepository(NotificationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(NotificationRoutingRule routingRule, CancellationToken cancellationToken = default)
    {
        await _dbContext.NotificationRoutingRules.AddAsync(routingRule, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<NotificationRoutingRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.NotificationRoutingRules
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<NotificationRoutingRule?> GetBestActiveRuleAsync(
        NotificationMessageType messageType,
        NotificationChannel channel,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.NotificationRoutingRules
            .AsNoTracking()
            .Where(x => x.MessageType == messageType && x.Channel == channel && x.IsActive)
            .OrderBy(x => x.Priority)
            .ThenBy(x => x.CreatedAtUtc)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
