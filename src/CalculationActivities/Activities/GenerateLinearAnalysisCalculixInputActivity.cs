using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CalculationActivities.Activities;

public class GenerateLinearAnalysisCalculixInputActivity : ActivityBase
{
    public GenerateLinearAnalysisCalculixInputActivity(ILogger<GenerateLinearAnalysisCalculixInputActivity> logger, IActivityHandler activityHandler)
        : base(logger, activityHandler)
    {
    }
}
