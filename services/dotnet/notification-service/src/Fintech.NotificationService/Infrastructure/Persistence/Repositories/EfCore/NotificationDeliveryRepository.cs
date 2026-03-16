using Fintech.NotificationService.Application.Abstractions;
using Fintech.NotificationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fintech.NotificationService.Infrastructure.Persistence.Repositories.EfCore;

public sealed class NotificationDeliveryRepository : INotificationDeliveryRepository
{
    private readonly NotificationDbContext _dbContext;

    public NotificationDeliveryRepository(NotificationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddEmailDeliveryAsync(
        NotificationDelivery delivery,
        EmailDelivery emailDelivery,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.NotificationDeliveries.AddAsync(delivery, cancellationToken);
        await _dbContext.EmailDeliveries.AddAsync(emailDelivery, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddSmsDeliveryAsync(
        NotificationDelivery delivery,
        SmsDelivery smsDelivery,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.NotificationDeliveries.AddAsync(delivery, cancellationToken);
        await _dbContext.SmsDeliveries.AddAsync(smsDelivery, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRealtimeDeliveryAsync(
        NotificationDelivery delivery,
        RealtimeDelivery realtimeDelivery,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.NotificationDeliveries.AddAsync(delivery, cancellationToken);
        await _dbContext.RealtimeDeliveries.AddAsync(realtimeDelivery, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<NotificationDelivery?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.NotificationDeliveries
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<EmailDelivery?> GetEmailDeliveryByNotificationDeliveryIdAsync(
        Guid notificationDeliveryId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.EmailDeliveries
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.NotificationDeliveryId == notificationDeliveryId, cancellationToken);
    }

    public Task<SmsDelivery?> GetSmsDeliveryByNotificationDeliveryIdAsync(
        Guid notificationDeliveryId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.SmsDeliveries
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.NotificationDeliveryId == notificationDeliveryId, cancellationToken);
    }

    public Task<RealtimeDelivery?> GetRealtimeDeliveryByNotificationDeliveryIdAsync(
        Guid notificationDeliveryId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.RealtimeDeliveries
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.NotificationDeliveryId == notificationDeliveryId, cancellationToken);
    }
}
