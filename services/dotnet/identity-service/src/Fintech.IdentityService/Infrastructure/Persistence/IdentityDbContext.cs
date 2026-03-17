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

    public DbSet<Group> Groups => Set<Group>();

    public DbSet<Permission> Permissions => Set<Permission>();

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

    public DbSet<User> Users => Set<User>();

    public DbSet<UserGroup> UserGroups => Set<UserGroup>();

    public DbSet<UserRole> UserRoles => Set<UserRole>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }
}
