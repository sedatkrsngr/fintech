using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.Enums;
using Fintech.IdentityService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Fintech.IdentityService.Infrastructure.Persistence.Repositories.EfCore;

public sealed class AuthorizationRepository : IAuthorizationRepository
{
    private readonly IdentityDbContext _dbContext;

    public AuthorizationRepository(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAccessRuleAsync(AccessRule accessRule, CancellationToken cancellationToken = default)
    {
        await _dbContext.AccessRules.AddAsync(accessRule, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddGroupAsync(Group group, CancellationToken cancellationToken = default)
    {
        await _dbContext.Groups.AddAsync(group, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddPermissionAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        await _dbContext.Permissions.AddAsync(permission, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRoleAsync(Role role, CancellationToken cancellationToken = default)
    {
        await _dbContext.Roles.AddAsync(role, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRolePermissionAsync(RolePermission rolePermission, CancellationToken cancellationToken = default)
    {
        await _dbContext.RolePermissions.AddAsync(rolePermission, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddUserGroupAsync(UserGroup userGroup, CancellationToken cancellationToken = default)
    {
        await _dbContext.UserGroups.AddAsync(userGroup, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddUserRoleAsync(UserRole userRole, CancellationToken cancellationToken = default)
    {
        await _dbContext.UserRoles.AddAsync(userRole, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<AccessRule?> GetAccessRuleByIdAsync(AccessRuleId accessRuleId, CancellationToken cancellationToken = default)
    {
        return _dbContext.AccessRules
            .FirstOrDefaultAsync(x => x.Id == accessRuleId, cancellationToken);
    }

    public Task<Group?> GetGroupByIdAsync(GroupId groupId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Groups
            .FirstOrDefaultAsync(x => x.Id == groupId, cancellationToken);
    }

    public Task<Permission?> GetPermissionByIdAsync(PermissionId permissionId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Permissions
            .FirstOrDefaultAsync(x => x.Id == permissionId, cancellationToken);
    }

    public Task<Role?> GetRoleByIdAsync(RoleId roleId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Roles
            .FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken);
    }

    public async Task<IReadOnlyList<AccessRule>> GetAccessRulesAsync(
        AccessRuleSubjectType subjectType,
        IReadOnlyCollection<Guid> subjectIds,
        CancellationToken cancellationToken = default)
    {
        if (subjectIds.Count == 0)
        {
            return Array.Empty<AccessRule>();
        }

        return await _dbContext.AccessRules
            .Where(x => x.SubjectType == subjectType && subjectIds.Contains(x.SubjectId) && x.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Guid>> GetRoleIdsByUserIdAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserRoles
            .Where(x => x.UserId == userId)
            .Select(x => x.RoleId.Value)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Guid>> GetGroupIdsByUserIdAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserGroups
            .Where(x => x.UserId == userId)
            .Select(x => x.GroupId.Value)
            .ToListAsync(cancellationToken);
    }
}
