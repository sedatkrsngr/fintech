using Fintech.NotificationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.NotificationService.Infrastructure.Persistence.Configurations;

public sealed class RealtimeDeliveryConfiguration : IEntityTypeConfiguration<RealtimeDelivery>
{
    public void Configure(EntityTypeBuilder<RealtimeDelivery> builder)
    {
        builder.ToTable("realtime_deliveries");

        builder.HasKey(x => x.NotificationDeliveryId);

        builder.Property(x => x.NotificationDeliveryId)
            .ValueGeneratedNever();

        builder.Property(x => x.TargetUserId)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.Payload)
            .IsRequired();
    }
}
