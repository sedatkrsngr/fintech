namespace Fintech.IdentityService.Api.Requests;

public sealed record AssignRoleToUserRequest(Guid UserId, Guid RoleId);
