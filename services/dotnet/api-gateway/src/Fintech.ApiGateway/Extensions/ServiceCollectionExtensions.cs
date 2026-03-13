using Fintech.ApiGateway.Configuration;
using Fintech.ApiGateway.Middleware;

namespace Fintech.ApiGateway.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiGatewayServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.Configure<GatewayOptions>(configuration.GetSection(GatewayOptions.SectionName));
        services.AddTransient<CorrelationIdMiddleware>();

        return services;
    }
}
