using System.Security.Claims;
using System.Text.Encodings.Web;
using Fintech.ApiGateway.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Fintech.ApiGateway.Infrastructure.Auth;

public sealed class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public const string SchemeName = "ApiKey";
    public const string HeaderName = "X-Api-Key";

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly InternalAuthOptions _internalAuthOptions;

    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IHttpClientFactory httpClientFactory,
        IOptions<InternalAuthOptions> internalAuthOptions)
        : base(options, logger, encoder)
    {
        _httpClientFactory = httpClientFactory;
        _internalAuthOptions = internalAuthOptions.Value;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(HeaderName, out var apiKeyHeader) ||
            string.IsNullOrWhiteSpace(apiKeyHeader))
        {
            return AuthenticateResult.NoResult();
        }

        var httpClient = _httpClientFactory.CreateClient("identity-internal-auth");
        var remoteIp = Context.Connection.RemoteIpAddress?.ToString();

        using var response = await httpClient.PostAsJsonAsync(
            "/api/internal/auth/validate-api-key",
            new ValidateApiKeyRequest(apiKeyHeader.ToString(), remoteIp),
            Context.RequestAborted);

        if (!response.IsSuccessStatusCode)
        {
            return AuthenticateResult.Fail("Invalid API key.");
        }

        var payload = await response.Content.ReadFromJsonAsync<ValidateApiKeyResponse>(cancellationToken: Context.RequestAborted);

        if (payload is null)
        {
            return AuthenticateResult.Fail("API key validation payload was empty.");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, payload.ApiClientId.ToString()),
            new(ClaimTypes.Name, payload.Name),
            new("subject_type", "api_client")
        };

        var identity = new ClaimsIdentity(claims, SchemeName);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, SchemeName);

        return AuthenticateResult.Success(ticket);
    }
}
