namespace Fintech.IdentityService.Application.Assignments.AssignRoleToUser;

public sealed record AssignRoleToUserCommand(Guid UserId, Guid RoleId);
