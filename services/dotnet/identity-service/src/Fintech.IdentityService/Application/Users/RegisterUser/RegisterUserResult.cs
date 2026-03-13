namespace Fintech.IdentityService.Application.Users.RegisterUser;

public sealed record RegisterUserResult(Guid UserId, string Email, DateTime CreatedAtUtc);
