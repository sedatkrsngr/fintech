namespace Fintech.IdentityService.Api.Responses;

public sealed record RoleResponse(Guid RoleId, string Name, DateTime CreatedAtUtc);
