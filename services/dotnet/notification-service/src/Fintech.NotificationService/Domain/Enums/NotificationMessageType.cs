namespace Fintech.NotificationService.Domain.Enums;

public enum NotificationMessageType
{
    UserRegistered = 1,
    OtpRequested = 2,
    TransferCompleted = 3,
    TransferFailed = 4,
    FraudAlert = 5
}
