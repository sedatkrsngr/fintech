using Fintech.NotificationService.Domain.Entities;
using Fintech.NotificationService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.NotificationService.Infrastructure.Persistence.Configurations;

public sealed class SmsDeliveryConfiguration : IEntityTypeConfiguration<SmsDelivery>
{
    public void Configure(EntityTypeBuilder<SmsDelivery> builder)
    {
        builder.ToTable("sms_deliveries");

        builder.HasKey(x => x.NotificationDeliveryId);

        builder.Property(x => x.NotificationDeliveryId)
            .ValueGeneratedNever();

        builder.Property(x => x.PhoneNumber)
            .HasConversion(
                phoneNumber => phoneNumber.Value,
                value => PhoneNumber.Create(value))
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.Message)
            .IsRequired();
    }
}
