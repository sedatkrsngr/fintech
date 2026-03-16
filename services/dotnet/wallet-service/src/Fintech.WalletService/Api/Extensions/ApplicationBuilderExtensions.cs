namespace Fintech.WalletService.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseApiPipeline(this IApplicationBuilder app)
    {
        return app;
    }
}
