using Fintech.NotificationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fintech.NotificationService.Infrastructure.Persistence;

public sealed class NotificationDbContext : DbContext
{
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options)
        : base(options)
    {
    }

    public DbSet<NotificationProvider> NotificationProviders => Set<NotificationProvider>();

    public DbSet<NotificationDelivery> NotificationDeliveries => Set<NotificationDelivery>();

    public DbSet<EmailDelivery> EmailDeliveries => Set<EmailDelivery>();

    public DbSet<SmsDelivery> SmsDeliveries => Set<SmsDelivery>();

    public DbSet<RealtimeDelivery> RealtimeDeliveries => Set<RealtimeDelivery>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);
    }
}
