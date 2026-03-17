using Fintech.IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.IdentityService.Infrastructure.Persistence.Configurations;

public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                tokenId => tokenId.Value,
                value => Domain.ValueObjects.RefreshTokenId.From(value))
            .ValueGeneratedNever();

        builder.Property(x => x.UserId)
            .HasConversion(
                userId => userId.Value,
                value => Domain.ValueObjects.UserId.From(value))
            .IsRequired();

        builder.Property(x => x.HashedToken)
            .HasConversion(
                hashedToken => hashedToken.Value,
                value => Domain.ValueObjects.HashedRefreshToken.Create(value))
            .HasMaxLength(512)
            .IsRequired();

        builder.Property(x => x.ExpiresAtUtc)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.Property(x => x.RevokedAtUtc);

        builder.Property(x => x.ReplacedByTokenId)
            .HasConversion(
                tokenId => tokenId.HasValue ? tokenId.Value.Value : (Guid?)null,
                value => value.HasValue ? Domain.ValueObjects.RefreshTokenId.From(value.Value) : null);

        builder.HasIndex(x => x.UserId);
    }
}
