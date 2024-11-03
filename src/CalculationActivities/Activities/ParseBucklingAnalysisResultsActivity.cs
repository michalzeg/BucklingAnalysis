using Microsoft.Extensions.Logging;

namespace CalculationActivities.Activities;

public class ParseBucklingAnalysisResultsActivity : ActivityBase
{
    public ParseBucklingAnalysisResultsActivity(ILogger<TriangulateActivity> logger , IActivityHandler activityHandler)
        : base(logger, activityHandler)
    {
    }
}
