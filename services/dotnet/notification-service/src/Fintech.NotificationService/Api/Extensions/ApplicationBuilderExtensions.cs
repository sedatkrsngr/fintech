namespace Fintech.NotificationService.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplication UseNotificationApi(this WebApplication app)
    {
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}
