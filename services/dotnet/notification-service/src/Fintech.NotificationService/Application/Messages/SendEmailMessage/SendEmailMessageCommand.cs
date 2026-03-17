using Fintech.Contracts.Notifications;

namespace Fintech.NotificationService.Application.Messages.SendEmailMessage;

public sealed record SendEmailMessageCommand(
    NotificationMessageType MessageType,
    string ToEmail,
    string Subject,
    string Body);
