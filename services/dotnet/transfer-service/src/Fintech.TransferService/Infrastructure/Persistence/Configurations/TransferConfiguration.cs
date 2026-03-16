using Fintech.TransferService.Domain.Entities;
using Fintech.TransferService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.TransferService.Infrastructure.Persistence.Configurations;

public sealed class TransferConfiguration : IEntityTypeConfiguration<Transfer>
{
    public void Configure(EntityTypeBuilder<Transfer> builder)
    {
        builder.ToTable("transfers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                transferId => transferId.Value,
                value => TransferId.From(value))
            .ValueGeneratedNever();

        builder.Property(x => x.SourceWalletId)
            .HasConversion(
                walletId => walletId.Value,
                value => WalletId.From(value))
            .IsRequired();

        builder.Property(x => x.DestinationWalletId)
            .HasConversion(
                walletId => walletId.Value,
                value => WalletId.From(value))
            .IsRequired();

        builder.Property(x => x.ReferenceId)
            .HasConversion(
                referenceId => referenceId.Value,
                value => ReferenceId.Create(value))
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.OwnsOne(x => x.Amount, amountBuilder =>
        {
            amountBuilder.Property(x => x.Amount)
                .HasColumnName("amount")
                .HasPrecision(18, 2)
                .IsRequired();

            amountBuilder.OwnsOne(x => x.Currency, currencyBuilder =>
            {
                currencyBuilder.Property(x => x.Code)
                    .HasColumnName("currency_code")
                    .HasMaxLength(3)
                    .IsRequired();
            });
        });
    }
}
