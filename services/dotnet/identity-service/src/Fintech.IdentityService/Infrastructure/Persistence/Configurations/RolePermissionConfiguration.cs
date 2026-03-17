using Fintech.IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.IdentityService.Infrastructure.Persistence.Configurations;

public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("role_permissions");

        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.Property(x => x.RoleId)
            .HasConversion(
                roleId => roleId.Value,
                value => Domain.ValueObjects.RoleId.From(value));

        builder.Property(x => x.PermissionId)
            .HasConversion(
                permissionId => permissionId.Value,
                value => Domain.ValueObjects.PermissionId.From(value));

        builder.Property(x => x.AssignedAtUtc)
            .IsRequired();
    }
}
