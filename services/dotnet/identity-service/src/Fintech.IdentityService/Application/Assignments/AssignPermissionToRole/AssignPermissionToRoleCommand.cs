namespace Fintech.IdentityService.Application.Assignments.AssignPermissionToRole;

public sealed record AssignPermissionToRoleCommand(Guid RoleId, Guid PermissionId);
