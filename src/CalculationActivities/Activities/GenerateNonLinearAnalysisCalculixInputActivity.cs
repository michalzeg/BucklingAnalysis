using Microsoft.Extensions.Logging;

namespace CalculationActivities.Activities;

public class GenerateNonLinearAnalysisCalculixInputActivity : ActivityBase
{
    public GenerateNonLinearAnalysisCalculixInputActivity(ILogger<GenerateNonLinearAnalysisCalculixInputActivity> logger, IActivityHandler activityHandler)
        : base(logger, activityHandler)
    {
    }
}
