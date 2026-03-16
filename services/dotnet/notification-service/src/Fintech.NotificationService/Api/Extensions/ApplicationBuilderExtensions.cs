using Fintech.NotificationService.Infrastructure.Providers.Realtime;

namespace Fintech.NotificationService.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplication UseNotificationApi(this WebApplication app)
    {
        app.UseAuthorization();
        app.MapControllers();
        app.MapHub<NotificationHub>("/hubs/notifications");

        return app;
    }
}
