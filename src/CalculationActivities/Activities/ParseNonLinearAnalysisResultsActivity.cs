using Microsoft.Extensions.Logging;

namespace CalculationActivities.Activities;

public class ParseNonLinearAnalysisResultsActivity : ActivityBase
{
    public ParseNonLinearAnalysisResultsActivity(ILogger<ParseNonLinearAnalysisResultsActivity> logger, IActivityHandler activityHandler)
        : base(logger, activityHandler)
    {
    }
}
