using Fintech.NotificationService.Application.Abstractions;
using Fintech.NotificationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fintech.NotificationService.Infrastructure.Persistence.Repositories.EfCore;

public sealed class NotificationProviderRepository : INotificationProviderRepository
{
    private readonly NotificationDbContext _dbContext;

    public NotificationProviderRepository(NotificationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(NotificationProvider provider, CancellationToken cancellationToken = default)
    {
        await _dbContext.NotificationProviders.AddAsync(provider, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<NotificationProvider?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.NotificationProviders
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
