
using CalculationActivities.Activities;
using MassTransit;
using MassTransit.Courier.Contracts;
using SagaCoordinator.Saga;
using Shared.Notifications;
using Shared.Storage;

namespace SagaCoordinator.Effects;

public class RoutingSlipActivityCompletedEffect : IEffect<RoutingSlipActivityCompleted>
{
    private readonly ILogger<RoutingSlipActivityCompletedEffect> _logger;
    private readonly IStorage _storage;

    public RoutingSlipActivityCompletedEffect(ILogger<RoutingSlipActivityCompletedEffect> logger, IStorage storage)
    {
        _logger = logger;
        _storage = storage;
    }

    public void Then(BehaviorContext<CalculationsCoordinatorState, RoutingSlipActivityCompleted> context)
    {
        _logger.LogInformation("SAGA: Activity completed. Message: {message}. Duration: {duration}", context.Message.ActivityName, context.Message.Duration);
    }

    public async Task ThenAsync(BehaviorContext<CalculationsCoordinatorState, RoutingSlipActivityCompleted> context)
    {
        context.Saga.CompletedActivities.Add(context.Message.ActivityName);
        context.Saga.ActivityIndex++;

        await _storage.SetAsync(StorageConstants.CompletedActivityNames, context.Saga.CompletedActivities, context.Message.TrackingNumber);

        await context.Publish(new ActivityCompletedNotification(context.Message.TrackingNumber, context.Message.ActivityName));

        var task = context.Message.ActivityName switch
        {
            nameof(GenerateFacadeActivity) => context.Publish<FacadeGeneratedNotification>(new(context.Message.TrackingNumber)),
            nameof(FilterLinearAnalysisResultsActivity) => context.Publish<LinearAnalysisResultsReadyNotification>(new(context.Message.TrackingNumber)),
            nameof(FilterBucklingAnalysisResultsActivity) => context.Publish<BucklingAnalysisReadyNotification>(new(context.Message.TrackingNumber)),
            nameof(FilterNonLinearAnalysisResultsActivity) => context.Publish<NonLinearAnalysisResultsReadyNotification>(new(context.Message.TrackingNumber)),
            _ => Task.CompletedTask
        };
        await task;
    }

}
