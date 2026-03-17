namespace Fintech.IdentityService.Api.Requests;

public sealed record IssueTokenRequest(string Email, string Password);
