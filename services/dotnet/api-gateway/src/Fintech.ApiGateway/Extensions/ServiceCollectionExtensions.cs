using Fintech.ApiGateway.Configuration;
using Fintech.ApiGateway.Middleware;
using Yarp.ReverseProxy.Transforms;

namespace Fintech.ApiGateway.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiGatewayServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.Configure<GatewayOptions>(configuration.GetSection(GatewayOptions.SectionName));
        services.AddReverseProxy()
            .LoadFromConfig(configuration.GetSection("ReverseProxy"))
            .AddTransforms(transformBuilderContext =>
            {
                transformBuilderContext.AddRequestTransform(transformContext =>
                {
                    var correlationId = transformContext.HttpContext.TraceIdentifier;

                    if (!string.IsNullOrWhiteSpace(correlationId))
                    {
                        transformContext.ProxyRequest.Headers.Remove(CorrelationIdMiddleware.HeaderName);
                        transformContext.ProxyRequest.Headers.TryAddWithoutValidation(
                            CorrelationIdMiddleware.HeaderName,
                            correlationId);
                    }

                    return ValueTask.CompletedTask;
                });
            });

        return services;
    }
}
