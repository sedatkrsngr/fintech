using Fintech.IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.IdentityService.Infrastructure.Persistence.Configurations;

public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                roleId => roleId.Value,
                value => Domain.ValueObjects.RoleId.From(value))
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasConversion(
                roleName => roleName.Value,
                value => Domain.ValueObjects.RoleName.Create(value))
            .HasMaxLength(128)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();
    }
}
