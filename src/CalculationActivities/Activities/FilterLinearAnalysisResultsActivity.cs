using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CalculationActivities.Activities;

public class FilterLinearAnalysisResultsActivity : ActivityBase
{
    public FilterLinearAnalysisResultsActivity(ILogger<FilterLinearAnalysisResultsActivity> logger, IActivityHandler activityHandler)
        : base(logger, activityHandler)
    {
    }
}
