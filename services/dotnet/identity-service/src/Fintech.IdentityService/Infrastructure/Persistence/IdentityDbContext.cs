using Fintech.IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fintech.IdentityService.Infrastructure.Persistence;

public sealed class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
    }

    public DbSet<AccessRule> AccessRules => Set<AccessRule>();

    public DbSet<ApiClient> ApiClients => Set<ApiClient>();

    public DbSet<ApiClientAllowedIp> ApiClientAllowedIps => Set<ApiClientAllowedIp>();

    public DbSet<ApiKey> ApiKeys => Set<ApiKey>();

    public DbSet<EmailVerificationToken> EmailVerificationTokens => Set<EmailVerificationToken>();

    public DbSet<Group> Groups => Set<Group>();

    public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();

    public DbSet<Permission> Permissions => Set<Permission>();

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<User> Users => Set<User>();

    public DbSet<UserGroup> UserGroups => Set<UserGroup>();

    public DbSet<UserRole> UserRoles => Set<UserRole>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }
}
