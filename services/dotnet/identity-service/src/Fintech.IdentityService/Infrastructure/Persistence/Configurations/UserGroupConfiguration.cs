using Fintech.IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.IdentityService.Infrastructure.Persistence.Configurations;

public sealed class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        builder.ToTable("user_groups");

        builder.HasKey(x => new { x.UserId, x.GroupId });

        builder.Property(x => x.UserId)
            .HasConversion(
                userId => userId.Value,
                value => Domain.ValueObjects.UserId.From(value));

        builder.Property(x => x.GroupId)
            .HasConversion(
                groupId => groupId.Value,
                value => Domain.ValueObjects.GroupId.From(value));

        builder.Property(x => x.AssignedAtUtc)
            .IsRequired();
    }
}
