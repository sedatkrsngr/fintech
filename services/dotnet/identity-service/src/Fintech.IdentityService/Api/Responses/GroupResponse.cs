namespace Fintech.IdentityService.Api.Responses;

public sealed record GroupResponse(Guid GroupId, string Name, DateTime CreatedAtUtc);
