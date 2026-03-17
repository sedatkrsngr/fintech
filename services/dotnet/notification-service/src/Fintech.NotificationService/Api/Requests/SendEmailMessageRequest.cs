using Fintech.Contracts.Notifications;

namespace Fintech.NotificationService.Api.Requests;

public sealed record SendEmailMessageRequest(
    NotificationMessageType MessageType,
    string ToEmail,
    string Subject,
    string Body);
