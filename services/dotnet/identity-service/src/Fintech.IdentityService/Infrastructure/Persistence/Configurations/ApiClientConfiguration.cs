using Fintech.IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.IdentityService.Infrastructure.Persistence.Configurations;

public sealed class ApiClientConfiguration : IEntityTypeConfiguration<ApiClient>
{
    public void Configure(EntityTypeBuilder<ApiClient> builder)
    {
        builder.ToTable("api_clients");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                apiClientId => apiClientId.Value,
                value => Domain.ValueObjects.ApiClientId.From(value))
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasConversion(
                clientName => clientName.Value,
                value => Domain.ValueObjects.ApiClientName.Create(value))
            .HasMaxLength(128)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();
    }
}
