using Fintech.NotificationService.Domain.Entities;
using Fintech.NotificationService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.NotificationService.Infrastructure.Persistence.Configurations;

public sealed class NotificationProviderConfiguration : IEntityTypeConfiguration<NotificationProvider>
{
    public void Configure(EntityTypeBuilder<NotificationProvider> builder)
    {
        builder.ToTable("notification_providers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.ProviderKey)
            .HasConversion(
                providerKey => providerKey.Value,
                value => ProviderKey.Create(value))
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.DisplayName)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.Channel)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.ProviderType)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.HasIndex(x => x.ProviderKey)
            .IsUnique();

        builder.Ignore(x => x.CreatedEvent);
    }
}
