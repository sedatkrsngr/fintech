using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Application.Auth.IssueToken;
using Fintech.IdentityService.Application.Users.GetUserById;
using Fintech.IdentityService.Application.Users.RegisterUser;
using Fintech.IdentityService.Configuration;
using Fintech.IdentityService.Infrastructure.Persistence;
using Fintech.IdentityService.Infrastructure.Persistence.Repositories.EfCore;
using Fintech.IdentityService.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

namespace Fintech.IdentityService.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("IdentityDatabase")
            ?? throw new InvalidOperationException("Connection string 'IdentityDatabase' is missing.");

        services.AddControllers();
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services.AddDbContext<IdentityDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IPasswordHashingService, PasswordHashingService>();
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddTransient<IssueTokenHandler>();
        services.AddTransient<GetUserByIdHandler>();
        services.AddTransient<RegisterUserHandler>();

        return services;
    }
}
