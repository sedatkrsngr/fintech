using Fintech.ApiGateway.Middleware;

namespace Fintech.ApiGateway.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseApiGatewayPipeline(this IApplicationBuilder app)
    {
        app.UseMiddleware<CorrelationIdMiddleware>();

        return app;
    }
}
