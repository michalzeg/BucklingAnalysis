using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CalculationActivities.Activities;

public class ParseLinearAnalysisResultsActivity : ActivityBase
{
    public ParseLinearAnalysisResultsActivity(ILogger<ParseLinearAnalysisResultsActivity> logger , IActivityHandler activityHandler)
        : base(logger, activityHandler)
    {
    }
}
