using Fintech.IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.IdentityService.Infrastructure.Persistence.Configurations;

public sealed class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
{
    public void Configure(EntityTypeBuilder<ApiKey> builder)
    {
        builder.ToTable("api_keys");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                apiKeyId => apiKeyId.Value,
                value => Domain.ValueObjects.ApiKeyId.From(value))
            .ValueGeneratedNever();

        builder.Property(x => x.ApiClientId)
            .HasConversion(
                apiClientId => apiClientId.Value,
                value => Domain.ValueObjects.ApiClientId.From(value))
            .IsRequired();

        builder.Property(x => x.HashedKey)
            .HasConversion(
                hashedKey => hashedKey.Value,
                value => Domain.ValueObjects.HashedApiKey.Create(value))
            .HasMaxLength(512)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.ExpiresAtUtc);

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();
    }
}
