using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CalculationActivities.Activities;

public class GenerateBucklingAnalysisCalculixInputActivity : ActivityBase
{
    public GenerateBucklingAnalysisCalculixInputActivity(ILogger<GenerateBucklingAnalysisCalculixInputActivity> logger, IActivityHandler activityHandler)
        : base(logger, activityHandler)
    {
    }
}
