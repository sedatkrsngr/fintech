namespace Fintech.IdentityService.Application.Roles.CreateRole;

public sealed record CreateRoleResult(Guid RoleId, string Name, DateTime CreatedAtUtc);
