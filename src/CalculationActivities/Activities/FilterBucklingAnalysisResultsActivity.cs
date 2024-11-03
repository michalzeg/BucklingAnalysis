using Microsoft.Extensions.Logging;

namespace CalculationActivities.Activities;

public class FilterBucklingAnalysisResultsActivity : ActivityBase
{
    public FilterBucklingAnalysisResultsActivity(ILogger<FilterBucklingAnalysisResultsActivity> logger, IActivityHandler activityHandler)
        : base(logger, activityHandler)
    {
    }
}
