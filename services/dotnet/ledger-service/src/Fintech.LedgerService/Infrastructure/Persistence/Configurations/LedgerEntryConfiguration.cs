using Fintech.LedgerService.Domain.Entities;
using Fintech.LedgerService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.LedgerService.Infrastructure.Persistence.Configurations;

public sealed class LedgerEntryConfiguration : IEntityTypeConfiguration<LedgerEntry>
{
    public void Configure(EntityTypeBuilder<LedgerEntry> builder)
    {
        builder.ToTable("ledger_entries");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                ledgerEntryId => ledgerEntryId.Value,
                value => LedgerEntryId.From(value))
            .ValueGeneratedNever();

        builder.Property(x => x.WalletId)
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

        builder.Property(x => x.EntryType)
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
