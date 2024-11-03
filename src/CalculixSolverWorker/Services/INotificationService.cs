
namespace CalculixSolverWorker.Services;

public interface INotificationService
{
    void SendProgress(Guid trackingNumber, string? value);
}