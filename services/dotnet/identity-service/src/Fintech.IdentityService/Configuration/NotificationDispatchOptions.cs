namespace Fintech.IdentityService.Configuration;

public sealed class NotificationDispatchOptions
{
    public const string SectionName = "NotificationDispatch";

    public string BaseUrl { get; init; } = "http://localhost:5005";
}
