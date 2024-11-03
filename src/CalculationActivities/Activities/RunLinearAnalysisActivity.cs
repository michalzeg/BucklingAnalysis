using Microsoft.Extensions.Logging;

namespace CalculationActivities.Activities;

public class RunLinearAnalysisActivity : ActivityBase
{
    public RunLinearAnalysisActivity(ILogger<RunLinearAnalysisActivity> logger , IActivityHandler activityHandler)
        : base(logger, activityHandler)
    {
    }
}
