using Fintech.NotificationService.Domain.Entities;
using Fintech.NotificationService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.NotificationService.Infrastructure.Persistence.Configurations;

public sealed class EmailDeliveryConfiguration : IEntityTypeConfiguration<EmailDelivery>
{
    public void Configure(EntityTypeBuilder<EmailDelivery> builder)
    {
        builder.ToTable("email_deliveries");

        builder.HasKey(x => x.NotificationDeliveryId);

        builder.Property(x => x.NotificationDeliveryId)
            .ValueGeneratedNever();

        builder.Property(x => x.ToEmail)
            .HasConversion(
                email => email.Value,
                value => RecipientEmail.Create(value))
            .HasMaxLength(320)
            .IsRequired();

        builder.Property(x => x.Subject)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.Body)
            .IsRequired();
    }
}
