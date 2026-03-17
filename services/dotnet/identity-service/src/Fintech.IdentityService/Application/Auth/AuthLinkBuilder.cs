namespace Fintech.IdentityService.Application.Auth;

internal static class AuthLinkBuilder
{
    public static string Build(string baseUrl, string token)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            throw new ArgumentException("Base url is required.", nameof(baseUrl));
        }

        if (string.IsNullOrWhiteSpace(token))
        {
            throw new ArgumentException("Token is required.", nameof(token));
        }

        var separator = baseUrl.Contains('?', StringComparison.Ordinal) ? "&" : "?";
        return baseUrl + separator + "token=" + Uri.EscapeDataString(token);
    }
}
