using Microsoft.Extensions.Logging;

namespace CalculationActivities.Activities;

public class RunNonLinearAnalysisActivity : ActivityBase
{
    public RunNonLinearAnalysisActivity(ILogger<RunNonLinearAnalysisActivity> logger, IActivityHandler activityHandler)
        : base(logger, activityHandler)
    {
    }
}
