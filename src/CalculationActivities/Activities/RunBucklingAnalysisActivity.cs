using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CalculationActivities.Activities;

public class RunBucklingAnalysisActivity : ActivityBase
{
    public RunBucklingAnalysisActivity(ILogger<RunBucklingAnalysisActivity> logger , IActivityHandler activityHandler)
        : base(logger, activityHandler)
    {
    }
}
