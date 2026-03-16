using Fintech.NotificationService.Domain.Entities;

namespace Fintech.NotificationService.Application.Abstractions;

public interface INotificationProviderRepository
{
    Task AddAsync(NotificationProvider provider, CancellationToken cancellationToken = default);

    Task<NotificationProvider?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
