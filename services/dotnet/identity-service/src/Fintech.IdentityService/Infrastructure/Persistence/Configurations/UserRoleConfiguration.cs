using Fintech.IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.IdentityService.Infrastructure.Persistence.Configurations;

public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("user_roles");

        builder.HasKey(x => new { x.UserId, x.RoleId });

        builder.Property(x => x.UserId)
            .HasConversion(
                userId => userId.Value,
                value => Domain.ValueObjects.UserId.From(value));

        builder.Property(x => x.RoleId)
            .HasConversion(
                roleId => roleId.Value,
                value => Domain.ValueObjects.RoleId.From(value));

        builder.Property(x => x.AssignedAtUtc)
            .IsRequired();
    }
}
