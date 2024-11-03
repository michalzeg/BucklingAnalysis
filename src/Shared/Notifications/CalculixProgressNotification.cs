namespace Shared.Notifications;

public readonly record struct CalculixProgressValue(int Iteration, double Error, double Limit);

public record CalculixProgressNotification(Guid TrackingNumber, CalculixProgressValue Progress) : BaseNotification(TrackingNumber);
