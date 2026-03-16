namespace Fintech.NotificationService.Configuration;

public sealed class MailhogOptions
{
    public string Host { get; set; } = "localhost";

    public int Port { get; set; } = 1025;

    public string FromEmail { get; set; } = "noreply@fintech.local";
}
