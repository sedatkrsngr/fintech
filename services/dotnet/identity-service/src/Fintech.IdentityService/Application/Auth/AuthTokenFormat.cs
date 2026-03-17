using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Application.Auth;

internal static class AuthTokenFormat
{
    public static string BuildRefreshToken(RefreshTokenId refreshTokenId, string secret)
        => $"rt_{refreshTokenId.Value:N}.{secret}";

    public static bool TryParseRefreshToken(string value, out RefreshTokenId refreshTokenId, out string secret)
        => TryParse(value, "rt_", guid => RefreshTokenId.From(guid), out refreshTokenId, out secret);

    public static string BuildPasswordResetToken(PasswordResetTokenId passwordResetTokenId, string secret)
        => $"prt_{passwordResetTokenId.Value:N}.{secret}";

    public static bool TryParsePasswordResetToken(string value, out PasswordResetTokenId passwordResetTokenId, out string secret)
        => TryParse(value, "prt_", guid => PasswordResetTokenId.From(guid), out passwordResetTokenId, out secret);

    public static string BuildEmailVerificationToken(EmailVerificationTokenId emailVerificationTokenId, string secret)
        => $"evt_{emailVerificationTokenId.Value:N}.{secret}";

    public static bool TryParseEmailVerificationToken(
        string value,
        out EmailVerificationTokenId emailVerificationTokenId,
        out string secret)
        => TryParse(value, "evt_", guid => EmailVerificationTokenId.From(guid), out emailVerificationTokenId, out secret);

    private static bool TryParse<T>(
        string value,
        string prefix,
        Func<Guid, T> factory,
        out T tokenId,
        out string secret)
    {
        tokenId = default!;
        secret = string.Empty;

        if (string.IsNullOrWhiteSpace(value) || !value.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        var parts = value[prefix.Length..].Split('.', 2, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2 || !Guid.TryParseExact(parts[0], "N", out var guid))
        {
            return false;
        }

        tokenId = factory(guid);
        secret = parts[1];
        return !string.IsNullOrWhiteSpace(secret);
    }
}
