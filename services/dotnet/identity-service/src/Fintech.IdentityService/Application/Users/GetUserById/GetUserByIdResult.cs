namespace Fintech.IdentityService.Application.Users.GetUserById;

public sealed record GetUserByIdResult(Guid UserId, string Email, string Status, DateTime CreatedAtUtc);
