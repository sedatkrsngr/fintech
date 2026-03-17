namespace Fintech.IdentityService.Application.Auth.IssueToken;

public sealed record IssueTokenCommand(string Email, string Password);
