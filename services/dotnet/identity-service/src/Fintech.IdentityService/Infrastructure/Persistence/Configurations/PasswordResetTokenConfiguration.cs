using Fintech.IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.IdentityService.Infrastructure.Persistence.Configurations;

public sealed class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
{
    public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
    {
        builder.ToTable("password_reset_tokens");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                tokenId => tokenId.Value,
                value => Domain.ValueObjects.PasswordResetTokenId.From(value))
            .ValueGeneratedNever();

        builder.Property(x => x.UserId)
            .HasConversion(
                userId => userId.Value,
                value => Domain.ValueObjects.UserId.From(value))
            .IsRequired();

        builder.Property(x => x.HashedToken)
            .HasConversion(
                hashedToken => hashedToken.Value,
                value => Domain.ValueObjects.HashedPasswordResetToken.Create(value))
            .HasMaxLength(512)
            .IsRequired();

        builder.Property(x => x.ExpiresAtUtc)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.Property(x => x.ConsumedAtUtc);

        builder.HasIndex(x => x.UserId);
    }
}
