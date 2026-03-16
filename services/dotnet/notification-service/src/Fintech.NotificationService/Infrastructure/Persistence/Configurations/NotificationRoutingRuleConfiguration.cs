using Fintech.NotificationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.NotificationService.Infrastructure.Persistence.Configurations;

public sealed class NotificationRoutingRuleConfiguration : IEntityTypeConfiguration<NotificationRoutingRule>
{
    public void Configure(EntityTypeBuilder<NotificationRoutingRule> builder)
    {
        builder.ToTable("notification_routing_rules");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.MessageType)
            .HasConversion<string>()
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(x => x.Channel)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.ProviderId)
            .IsRequired();

        builder.Property(x => x.Priority)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.HasIndex(x => new { x.MessageType, x.Channel, x.Priority });
    }
}
