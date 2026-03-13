using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Application.Users.GetUserById;
using Fintech.IdentityService.Application.Users.RegisterUser;
using Fintech.IdentityService.Infrastructure.Persistence;
using Fintech.IdentityService.Infrastructure.Persistence.Repositories.EfCore;
using Microsoft.EntityFrameworkCore;

namespace Fintech.IdentityService.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("IdentityDatabase")
            ?? throw new InvalidOperationException("Connection string 'IdentityDatabase' is missing.");

        services.AddControllers();
        services.AddDbContext<IdentityDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddTransient<GetUserByIdHandler>();
        services.AddTransient<RegisterUserHandler>();

        return services;
    }
}
