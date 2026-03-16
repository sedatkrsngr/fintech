using Fintech.WalletService.Domain.Entities;
using Fintech.WalletService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.WalletService.Infrastructure.Persistence.Configurations;

public sealed class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("wallets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                walletId => walletId.Value,
                value => WalletId.From(value))
            .ValueGeneratedNever();

        builder.Property(x => x.OwnerUserId)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.Ignore(x => x.CreatedEvent);

        builder.OwnsOne(x => x.Balance, balanceBuilder =>
        {
            balanceBuilder.Property(x => x.Amount)
                .HasColumnName("balance_amount")
                .HasPrecision(18, 2)
                .IsRequired();

            balanceBuilder.OwnsOne(x => x.Currency, currencyBuilder =>
            {
                currencyBuilder.Property(x => x.Code)
                    .HasColumnName("currency_code")
                    .HasMaxLength(3)
                    .IsRequired();
            });
        });
    }
}
