using Microsoft.Extensions.Logging;

namespace CalculationActivities.Activities;

public class FilterNonLinearAnalysisResultsActivity : ActivityBase
{
    public FilterNonLinearAnalysisResultsActivity(ILogger<FilterNonLinearAnalysisResultsActivity> logger, IActivityHandler activityHandler)
        : base(logger, activityHandler)
    {
    }
}