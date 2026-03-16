namespace Fintech.NotificationService.Configuration;

public sealed class MailpitOptions
{
    public string Host { get; set; } = "localhost";

    public int Port { get; set; } = 1026;

    public string FromEmail { get; set; } = "noreply@fintech.local";
}
