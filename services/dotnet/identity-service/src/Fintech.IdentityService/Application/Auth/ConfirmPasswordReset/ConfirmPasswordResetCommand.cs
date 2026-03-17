namespace Fintech.IdentityService.Application.Auth.ConfirmPasswordReset;

public sealed record ConfirmPasswordResetCommand(string Token, string NewPassword);
