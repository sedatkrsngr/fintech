using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.Enums;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Application.Abstractions;

public interface IAuthorizationRepository
{
    Task AddAccessRuleAsync(AccessRule accessRule, CancellationToken cancellationToken = default);
    Task AddGroupAsync(Group group, CancellationToken cancellationToken = default);
    Task AddPermissionAsync(Permission permission, CancellationToken cancellationToken = default);
    Task AddRoleAsync(Role role, CancellationToken cancellationToken = default);
    Task AddRolePermissionAsync(RolePermission rolePermission, CancellationToken cancellationToken = default);
    Task AddUserGroupAsync(UserGroup userGroup, CancellationToken cancellationToken = default);
    Task AddUserRoleAsync(UserRole userRole, CancellationToken cancellationToken = default);

    Task<AccessRule?> GetAccessRuleByIdAsync(AccessRuleId accessRuleId, CancellationToken cancellationToken = default);
    Task<Group?> GetGroupByIdAsync(GroupId groupId, CancellationToken cancellationToken = default);
    Task<Permission?> GetPermissionByIdAsync(PermissionId permissionId, CancellationToken cancellationToken = default);
    Task<Role?> GetRoleByIdAsync(RoleId roleId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AccessRule>> GetAccessRulesAsync(
        AccessRuleSubjectType subjectType,
        IReadOnlyCollection<Guid> subjectIds,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Guid>> GetRoleIdsByUserIdAsync(UserId userId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Guid>> GetGroupIdsByUserIdAsync(UserId userId, CancellationToken cancellationToken = default);
}
