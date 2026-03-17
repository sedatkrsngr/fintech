using Fintech.IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.IdentityService.Infrastructure.Persistence.Configurations;

public sealed class EmailVerificationTokenConfiguration : IEntityTypeConfiguration<EmailVerificationToken>
{
    public void Configure(EntityTypeBuilder<EmailVerificationToken> builder)
    {
        builder.ToTable("email_verification_tokens");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                tokenId => tokenId.Value,
                value => Domain.ValueObjects.EmailVerificationTokenId.From(value))
            .ValueGeneratedNever();

        builder.Property(x => x.UserId)
            .HasConversion(
                userId => userId.Value,
                value => Domain.ValueObjects.UserId.From(value))
            .IsRequired();

        builder.Property(x => x.HashedToken)
            .HasConversion(
                hashedToken => hashedToken.Value,
                value => Domain.ValueObjects.HashedEmailVerificationToken.Create(value))
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
