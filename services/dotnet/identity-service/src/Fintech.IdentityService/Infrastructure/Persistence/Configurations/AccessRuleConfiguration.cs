using Fintech.IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.IdentityService.Infrastructure.Persistence.Configurations;

public sealed class AccessRuleConfiguration : IEntityTypeConfiguration<AccessRule>
{
    public void Configure(EntityTypeBuilder<AccessRule> builder)
    {
        builder.ToTable("access_rules");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                accessRuleId => accessRuleId.Value,
                value => Domain.ValueObjects.AccessRuleId.From(value))
            .ValueGeneratedNever();

        builder.Property(x => x.SubjectType)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.SubjectId)
            .IsRequired();

        builder.Property(x => x.Effect)
            .HasConversion<string>()
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(x => x.EndpointPattern)
            .HasConversion(
                endpointPattern => endpointPattern.Value,
                value => Domain.ValueObjects.EndpointPattern.Create(value))
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.HttpVerb)
            .HasConversion(
                httpVerb => httpVerb == null ? null : httpVerb.Value,
                value => string.IsNullOrWhiteSpace(value) ? null : Domain.ValueObjects.HttpVerb.Create(value))
            .HasMaxLength(16);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();
    }
}
