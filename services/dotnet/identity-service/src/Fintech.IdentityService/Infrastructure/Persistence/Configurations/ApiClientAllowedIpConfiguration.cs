using Fintech.IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.IdentityService.Infrastructure.Persistence.Configurations;

public sealed class ApiClientAllowedIpConfiguration : IEntityTypeConfiguration<ApiClientAllowedIp>
{
    public void Configure(EntityTypeBuilder<ApiClientAllowedIp> builder)
    {
        builder.ToTable("api_client_allowed_ips");

        builder.HasKey(x => new { x.ApiClientId, x.IpAddress });

        builder.Property(x => x.ApiClientId)
            .HasConversion(
                apiClientId => apiClientId.Value,
                value => Domain.ValueObjects.ApiClientId.From(value));

        builder.Property(x => x.IpAddress)
            .HasConversion(
                ipAddress => ipAddress.Value,
                value => Domain.ValueObjects.AllowedIpAddress.Create(value))
            .HasMaxLength(64);

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();
    }
}
