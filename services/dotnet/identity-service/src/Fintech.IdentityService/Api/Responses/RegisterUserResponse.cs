namespace Fintech.IdentityService.Api.Responses;

public sealed record RegisterUserResponse(Guid UserId, string Email, DateTime CreatedAtUtc);
