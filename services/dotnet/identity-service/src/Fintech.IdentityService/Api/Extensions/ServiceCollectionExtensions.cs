using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Application.AccessRules.CreateAccessRule;
using Fintech.IdentityService.Application.AccessRules.EvaluateAccess;
using Fintech.IdentityService.Application.AccessRules.GetAccessRuleById;
using Fintech.IdentityService.Application.AccessRules.ListAccessRulesBySubject;
using Fintech.IdentityService.Application.Assignments.AssignGroupToUser;
using Fintech.IdentityService.Application.Assignments.AssignPermissionToRole;
using Fintech.IdentityService.Application.Assignments.AssignRoleToUser;
using Fintech.IdentityService.Application.Auth.ConfirmEmailVerification;
using Fintech.IdentityService.Application.Auth.ConfirmPasswordReset;
using Fintech.IdentityService.Application.ApiAccess.CreateApiClient;
using Fintech.IdentityService.Application.ApiAccess.ValidateApiKey;
using Fintech.IdentityService.Application.Auth.IssueToken;
using Fintech.IdentityService.Application.Auth.RefreshAccessToken;
using Fintech.IdentityService.Application.Auth.RequestEmailVerification;
using Fintech.IdentityService.Application.Auth.RequestPasswordReset;
using Fintech.IdentityService.Application.Auth.RevokeRefreshToken;
using Fintech.IdentityService.Application.Groups.CreateGroup;
using Fintech.IdentityService.Application.Groups.GetGroupById;
using Fintech.IdentityService.Application.Permissions.CreatePermission;
using Fintech.IdentityService.Application.Permissions.GetPermissionById;
using Fintech.IdentityService.Application.Roles.CreateRole;
using Fintech.IdentityService.Application.Roles.GetRoleById;
using Fintech.IdentityService.Application.Users.GetUserById;
using Fintech.IdentityService.Application.Users.RegisterUser;
using Fintech.IdentityService.Configuration;
using Fintech.IdentityService.Infrastructure.Persistence;
using Fintech.IdentityService.Infrastructure.Persistence.Repositories.EfCore;
using Fintech.IdentityService.Infrastructure.Clients;
using Fintech.IdentityService.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Fintech.IdentityService.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("IdentityDatabase")
            ?? throw new InvalidOperationException("Connection string 'IdentityDatabase' is missing.");

        services.AddControllers();
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;

        services.AddOptions<AppLinksOptions>()
            .Bind(configuration.GetSection(AppLinksOptions.SectionName))
            .Validate(options => AreValidAbsoluteUrls(options.ResetPasswordBaseUrl, options.EmailVerificationBaseUrl),
                "AppLinks urls must be valid absolute urls.")
            .Validate(
                options => IsDevelopment(environmentName) ||
                           (!IsLocalhost(options.ResetPasswordBaseUrl) && !IsLocalhost(options.EmailVerificationBaseUrl)),
                "AppLinks localhost urls are only allowed in Development.")
            .ValidateOnStart();

        services.AddOptions<NotificationDispatchOptions>()
            .Bind(configuration.GetSection(NotificationDispatchOptions.SectionName))
            .Validate(options => AreValidAbsoluteUrls(options.BaseUrl), "NotificationDispatch.BaseUrl must be a valid absolute url.")
            .ValidateOnStart();

        services.Configure<AuthLifecycleOptions>(configuration.GetSection(AuthLifecycleOptions.SectionName));
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services.AddDbContext<IdentityDbContext>(options => options.UseNpgsql(connectionString));
        var notificationBaseUrl = configuration.GetSection(NotificationDispatchOptions.SectionName)
            .GetValue<string>(nameof(NotificationDispatchOptions.BaseUrl))
            ?? "http://localhost:5005";
        services.AddHttpClient<INotificationDispatchClient, NotificationDispatchClient>(client =>
        {
            client.BaseAddress = new Uri(notificationBaseUrl);
        });
        services.AddScoped<IAuthLifecycleRepository, AuthLifecycleRepository>();
        services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();
        services.AddScoped<IPasswordHashingService, PasswordHashingService>();
        services.AddScoped<ITokenSecretService, TokenSecretService>();
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<IApiAccessRepository, ApiAccessRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddTransient<ConfirmEmailVerificationHandler>();
        services.AddTransient<ConfirmPasswordResetHandler>();
        services.AddTransient<CreateAccessRuleHandler>();
        services.AddTransient<CreateApiClientHandler>();
        services.AddTransient<CreateGroupHandler>();
        services.AddTransient<CreatePermissionHandler>();
        services.AddTransient<CreateRoleHandler>();
        services.AddTransient<EvaluateAccessHandler>();
        services.AddTransient<AssignGroupToUserHandler>();
        services.AddTransient<AssignPermissionToRoleHandler>();
        services.AddTransient<AssignRoleToUserHandler>();
        services.AddTransient<GetAccessRuleByIdHandler>();
        services.AddTransient<GetGroupByIdHandler>();
        services.AddTransient<GetPermissionByIdHandler>();
        services.AddTransient<GetRoleByIdHandler>();
        services.AddTransient<ListAccessRulesBySubjectHandler>();
        services.AddTransient<RefreshAccessTokenHandler>();
        services.AddTransient<RequestEmailVerificationHandler>();
        services.AddTransient<RequestPasswordResetHandler>();
        services.AddTransient<RevokeRefreshTokenHandler>();
        services.AddTransient<ValidateApiKeyHandler>();
        services.AddTransient<IssueTokenHandler>();
        services.AddTransient<GetUserByIdHandler>();
        services.AddTransient<RegisterUserHandler>();

        return services;
    }

    private static bool AreValidAbsoluteUrls(params string[] values)
    {
        return values.All(value =>
            Uri.TryCreate(value, UriKind.Absolute, out var uri) &&
            !string.IsNullOrWhiteSpace(uri.Scheme) &&
            !string.IsNullOrWhiteSpace(uri.Host));
    }

    private static bool IsDevelopment(string environmentName)
    {
        return string.Equals(environmentName, "Development", StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsLocalhost(string value)
    {
        return Uri.TryCreate(value, UriKind.Absolute, out var uri) &&
               (string.Equals(uri.Host, "localhost", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(uri.Host, "127.0.0.1", StringComparison.OrdinalIgnoreCase));
    }
}
