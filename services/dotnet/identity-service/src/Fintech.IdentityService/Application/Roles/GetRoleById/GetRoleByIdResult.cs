namespace Fintech.IdentityService.Application.Roles.GetRoleById;

public sealed record GetRoleByIdResult(Guid RoleId, string Name, DateTime CreatedAtUtc);
