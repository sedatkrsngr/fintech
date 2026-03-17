using Fintech.IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.IdentityService.Infrastructure.Persistence.Configurations;

public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                permissionId => permissionId.Value,
                value => Domain.ValueObjects.PermissionId.From(value))
            .ValueGeneratedNever();

        builder.Property(x => x.Code)
            .HasConversion(
                permissionCode => permissionCode.Value,
                value => Domain.ValueObjects.PermissionCode.Create(value))
            .HasMaxLength(128)
            .IsRequired();

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();
    }
}
