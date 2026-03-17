namespace Fintech.IdentityService.Api.Requests;

public sealed record ConfirmPasswordResetRequest(string Token, string NewPassword);
