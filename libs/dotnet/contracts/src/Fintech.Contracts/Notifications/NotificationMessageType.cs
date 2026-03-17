namespace Fintech.Contracts.Notifications;

public enum NotificationMessageType
{
    UserRegistered = 1,
    OtpRequested = 2,
    TransferCompleted = 3,
    TransferFailed = 4,
    FraudAlert = 5,
    PasswordResetRequested = 6,
    EmailVerificationRequested = 7
}
