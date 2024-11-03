using Microsoft.Extensions.Logging;

namespace CalculationActivities.Activities;

public class NormalizeBucklingShapeActivity : ActivityBase
{
    public NormalizeBucklingShapeActivity(ILogger<NormalizeBucklingShapeActivity> logger, IActivityHandler activityHandler)
        : base(logger, activityHandler)
    {
    }
}
