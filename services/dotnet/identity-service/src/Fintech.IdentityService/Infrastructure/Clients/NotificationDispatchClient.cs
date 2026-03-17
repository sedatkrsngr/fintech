using System.Net.Http.Json;
using Fintech.Contracts.Notifications;
using Fintech.IdentityService.Application.Abstractions;

namespace Fintech.IdentityService.Infrastructure.Clients;

public sealed class NotificationDispatchClient : INotificationDispatchClient
{
    private readonly HttpClient _httpClient;

    public NotificationDispatchClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SendEmailAsync(
        NotificationMessageType messageType,
        string toEmail,
        string subject,
        string body,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "/api/notification-messages/email",
            new SendEmailMessageRequest(messageType, toEmail, subject, body),
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    private sealed record SendEmailMessageRequest(
        NotificationMessageType MessageType,
        string ToEmail,
        string Subject,
        string Body);
}
