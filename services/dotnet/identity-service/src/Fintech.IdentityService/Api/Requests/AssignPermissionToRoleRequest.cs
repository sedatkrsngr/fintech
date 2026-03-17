namespace Fintech.IdentityService.Api.Requests;

public sealed record AssignPermissionToRoleRequest(Guid RoleId, Guid PermissionId);
