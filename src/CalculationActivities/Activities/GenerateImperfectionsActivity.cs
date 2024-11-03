using Microsoft.Extensions.Logging;

namespace CalculationActivities.Activities;

public class GenerateImperfectionsActivity : ActivityBase
{
    public GenerateImperfectionsActivity(ILogger<GenerateImperfectionsActivity> logger, IActivityHandler activityHandler)
        : base(logger, activityHandler)
    {
    }
}
