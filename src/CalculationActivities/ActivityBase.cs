using CalculationActivities.ActivityArguments;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CalculationActivities;

public abstract class ActivityBase : IExecuteActivity<ActivityArgument>
{
    private readonly ILogger _logger;
    private readonly IActivityHandler _activityHandler;

    public ActivityBase(ILogger logger, IActivityHandler activityHandler)
    {
        _logger = logger;
        _activityHandler = activityHandler;
    }

    public async Task<ExecutionResult> Execute(ExecuteContext<ActivityArgument> context)
    {
        try
        {
            _logger.LogInformation("Starting executing activity for {name} with {message}", GetType().Name, context.Message.ToString());
            var activityContext = new ActivityContext(context.ActivityName, context.TrackingNumber);
            var result = await _activityHandler.Handle(activityContext, context.Arguments);

            _logger.LogInformation("Finished executing activity for {name}", GetType().Name);
            return context.Completed();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during activity {name}", context.ActivityName);
            throw;
        }
    }
}
